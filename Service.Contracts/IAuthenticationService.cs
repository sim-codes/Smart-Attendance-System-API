using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthenticationResponse> ValidateUser(UserForAuthenticationDto userForAuth);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);
        Task<string> GeneratePasswordResetTokenAsync(GenerateResetPasswordDto generateResetPassword);
        Task<IdentityResult> ResetPasswordAsync(ResetPasswordDto resetPassword);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
    }
}
