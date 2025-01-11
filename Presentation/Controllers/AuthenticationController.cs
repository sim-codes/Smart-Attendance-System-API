using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _service;

        public AuthenticationController(IServiceManager service) => _service = service;

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userForRegistration">The user data for registration</param>
        /// <returns>Status code 201 if the user is created successfully</returns>
        /// <response code="201">Returns status code 201 if the user is created successfully</response>
        /// <response code="400">If the user data is invalid</response>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            var result = await _service.AuthenticationService.RegisterUser(userForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        /// <summary>
        /// Authenticate a user and generate a token
        /// </summary>
        /// <param name="user">The user data for authentication</param>
        /// <returns>The authentication token and user profile</returns>
        /// <response code="200">Returns the authentication token and user profile</response>
        /// <response code="401">If the user credentials are invalid</response>
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(object), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserForAuthenticationDto user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDto = await _service.AuthenticationService.CreateToken(populateExp: true);
            var userProfile = await _service.ProfileService.GetUserByName(user.Username);

            return Ok(new { Token = tokenDto, User = userProfile });
        }

        /// <summary>
        /// Generate a password reset token
        /// </summary>
        /// <param name="generateResetPassword">The email of the user</param>
        /// <returns>The password reset token</returns>
        /// <response code="200">Returns the password reset token</response>
        /// <response code="404">If the user is not found</response>
        [HttpPost("generate-reset-token")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GeneratePasswordResetToken([FromBody] GenerateResetPasswordDto generateResetPassword)
        {
            var token = await _service.AuthenticationService.GeneratePasswordResetTokenAsync(generateResetPassword);
            if (token == null)
                return NotFound();
            return Ok(new { Message = "Token generated successfully"});
        }

        /// <summary>
        /// Reset the password
        /// </summary>
        /// <param name="resetPassword">The password reset data</param>
        /// <returns>Status code 200 if the password is reset successfully</returns>
        /// <response code="200">Returns status code 200 if the password is reset successfully</response>
        /// <response code="400">If the password reset data is invalid</response>
        /// <response code="404">If the user is not found</response>
        [HttpPost("reset-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPassword)
        {
            var result = await _service.AuthenticationService.ResetPasswordAsync(resetPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        /// <summary>
        /// Change the password
        /// </summary>
        /// <param name="changePasswordDto">The password change data</param>
        /// <returns>Status code 200 if the password is changed successfully</returns>
        /// <response code="200">Returns status code 200 if the password is changed successfully</response>
        /// <response code="400">If the password change data is invalid</response>
        /// <response code="404">If the user is not found</response>
        [HttpPost("change-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
        {
            var result = await _service.AuthenticationService.ChangePasswordAsync(changePasswordDto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
