using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Controllers
{
    public class AuthenticationControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly AuthenticationController _controller;

        public AuthenticationControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new AuthenticationController(_mockService.Object);
        }

        [Fact]
        public async Task RegisterUser_ReturnsStatusCode201_WhenUserIsCreated()
        {
            // Arrange
            var userForRegistration = new UserForRegistrationDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Password123!"
            };
            var identityResult = IdentityResult.Success;

            _mockService.Setup(s => s.AuthenticationService.RegisterUser(userForRegistration))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.RegisterUser(userForRegistration);

            // Assert
            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task RegisterUser_ReturnsBadRequest_WhenUserCreationFails()
        {
            // Arrange
            var userForRegistration = new UserForRegistrationDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Password123!"
            };
            var identityResult = IdentityResult.Failed(new IdentityError { Code = "Error", Description = "User creation failed" });

            _mockService.Setup(s => s.AuthenticationService.RegisterUser(userForRegistration))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.RegisterUser(userForRegistration);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(modelState.ContainsKey("Error"));
        }


        [Fact]
        public async Task GeneratePasswordResetToken_ReturnsNotFound_WhenUserIsNotFound()
        {
            // Arrange
            var generateResetPassword = new GenerateResetPasswordDto {Email = "testuser@example.com" };

            _mockService.Setup(s => s.AuthenticationService.GeneratePasswordResetTokenAsync(generateResetPassword))
                        .ReturnsAsync((string)null);

            // Act
            var result = await _controller.GeneratePasswordResetToken(generateResetPassword);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ResetPassword_ReturnsOkResult_WhenPasswordIsReset()
        {
            // Arrange
            var resetPassword = new ResetPasswordDto
            {
                Email = "testuser@example.com",
                Token = "reset-token",
                NewPassword = "NewPassword123!"
            };
            var identityResult = IdentityResult.Success;

            _mockService.Setup(s => s.AuthenticationService.ResetPasswordAsync(resetPassword))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ResetPassword(resetPassword);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ResetPassword_ReturnsBadRequest_WhenPasswordResetFails()
        {
            // Arrange
            var resetPassword = new ResetPasswordDto
            {
                Email = "testuser@example.com",
                Token = "reset-token",
                NewPassword = "NewPassword123!"
            };
            var identityResult = IdentityResult.Failed(new IdentityError { Code = "Error", Description = "Password reset failed" });

            _mockService.Setup(s => s.AuthenticationService.ResetPasswordAsync(resetPassword))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ResetPassword(resetPassword);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(modelState.ContainsKey("Error"));
        }

        [Fact]
        public async Task ChangePassword_ReturnsOkResult_WhenPasswordIsChanged()
        {
            // Arrange
            var changePasswordDto = new ChangePasswordDto
            {
                Email = "testuser@example.com",
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "NewPassword123!"
            };
            var identityResult = IdentityResult.Success;

            _mockService.Setup(s => s.AuthenticationService.ChangePasswordAsync(changePasswordDto))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ChangePassword_ReturnsBadRequest_WhenPasswordChangeFails()
        {
            // Arrange
            var changePasswordDto = new ChangePasswordDto
            {
                Email = "testuser@example.com",
                CurrentPassword = "CurrentPassword123!",
                NewPassword = "NewPassword123!"
            };
            var identityResult = IdentityResult.Failed(new IdentityError { Code = "Error", Description = "Password change failed" });

            _mockService.Setup(s => s.AuthenticationService.ChangePasswordAsync(changePasswordDto))
                        .ReturnsAsync(identityResult);

            // Act
            var result = await _controller.ChangePassword(changePasswordDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
            Assert.True(modelState.ContainsKey("Error"));
        }
    }
}
