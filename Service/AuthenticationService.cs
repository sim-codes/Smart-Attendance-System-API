using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class AuthenticationService : IAuthenticationService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IEmailService _emailService;

        private User? _user;

        public AuthenticationService(ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager, IConfiguration configuration,
            IEmailService emailService)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfiguration = new JwtConfiguration();
            _configuration.Bind(_jwtConfiguration.Section, _jwtConfiguration);
            _emailService = emailService;
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var user = _mapper.Map<User>(userForRegistration);

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);
            if (result.Succeeded)
                await _userManager.AddToRolesAsync(user, userForRegistration.Roles);

            return result;
        }

        public async Task<AuthenticationResponse> ValidateUser(UserForAuthenticationDto userForAuth)
        {
            var response = new AuthenticationResponse();

            if (string.IsNullOrWhiteSpace(userForAuth.Username))
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: Username is null or empty");
                response.IsAuthenticated = false;
                response.ErrorMessage = "Username is required";
                return response;
            }

            _user = await _userManager.FindByNameAsync(userForAuth.Username);

            if (_user == null)
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: User not found. Username: {userForAuth.Username}");
                response.IsAuthenticated = false;
                response.ErrorMessage = "User not found";
                return response;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(_user, userForAuth.Password);

            if (!passwordValid)
            {
                _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong password for user: {userForAuth.Username}");
                response.IsAuthenticated = false;
                response.ErrorMessage = "Invalid password";
                return response;
            }

            // Authentication successful
            response.IsAuthenticated = true;
            return response;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto(accessToken, refreshToken);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user?.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtConfiguration.ValidIssuer,
                audience: _jwtConfiguration.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtConfiguration.Expires)),
                signingCredentials: signingCredentials
            );
            return tokenOptions;

        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtConfiguration.SecretKey)),
                ValidateLifetime = false,
                ValidIssuer = _jwtConfiguration.ValidIssuer,
                ValidAudience = _jwtConfiguration.ValidAudience,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user == null || user.RefreshToken != tokenDto.RefreshToken
                || user.RefreshTokenExpiryTime <= DateTime.Now)
                throw new RefreshTokenBadRequest();

            _user = user;

            return await CreateToken(populateExp: false);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(GenerateResetPasswordDto generateResetPassword)
        {
            var user = await _userManager.FindByEmailAsync(generateResetPassword.Email);
            if (user is  null)
                throw new UserNotFoundException(generateResetPassword.Email);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var emailBody = GeneratePasswordResetEmailBody(generateResetPassword.Email, token);

            _emailService.SendEmail(generateResetPassword.Email, "Password Reset Request", emailBody);
            return token;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user is null)
                throw new UserNotFoundException(resetPassword.Email);

            var result = await _userManager.ResetPasswordAsync(user, resetPassword.Email, resetPassword.NewPassword);
            return result;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);
            if (user is null)
                throw new UserNotFoundException(changePasswordDto.Email);

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);
            return result;
        }

        private string GeneratePasswordResetEmailBody(string email, string token)
        {
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<p>Please reset your password by clicking the link below:</p>");
            mailBody.AppendFormat($"<a href=https://frontend-url/reset-verification?email={email}&token={token}>Reset password</a>");
            mailBody.AppendFormat("<p>If you did not request a password reset, please ignore this email</p>");
            mailBody.AppendFormat("<p>Thank you</p>");
            return mailBody.ToString();
        }

    }
}
