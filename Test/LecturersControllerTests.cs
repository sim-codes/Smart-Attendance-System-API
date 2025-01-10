using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Presentation.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.RequestFeatures;
using Microsoft.AspNetCore.Http;

namespace Tests
{
    public class LecturersControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly LecturersController _controller;

        public LecturersControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new LecturersController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllLecturers_ReturnsOkResult_WithListOfLecturers()
        {
            // Arrange
            var lecturerParameters = new LecturerParameters
            {
                PageNumber = 1,
                PageSize = 10,
                SearchTerm = null
            };

            var lecturers = new List<LecturerDto>
            {
                new LecturerDto { UserId = Guid.NewGuid().ToString(), StaffId = "12345" },
                new LecturerDto { UserId = Guid.NewGuid().ToString(), StaffId = "67890" }
            };

            var metaData = new MetaData
            {
                CurrentPage = lecturerParameters.PageNumber,
                PageSize = lecturerParameters.PageSize,
                TotalCount = lecturers.Count,
                TotalPages = 1
            };

            _mockService.Setup(s => s.LecturerService.GetLecturersAsync(lecturerParameters, false))
                        .ReturnsAsync((lecturers, metaData));

            // Set up HttpContext and Response headers
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = await _controller.GetLecturers(lecturerParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLecturers = Assert.IsAssignableFrom<IEnumerable<LecturerDto>>(okResult.Value);
            Assert.Equal(2, returnLecturers.Count());

            // Verify the pagination header was set
            Assert.True(httpContext.Response.Headers.ContainsKey("X-Pagination"));
        }

        [Fact]
        public async Task GetLecturer_ReturnsOkResult_WithLecturer()
        {
            // Arrange
            var lecturerId = Guid.NewGuid().ToString();
            var lecturer = new LecturerDto { UserId = lecturerId, StaffId = "12345" };
            _mockService.Setup(s => s.LecturerService.GetLecturerAsync(lecturerId, false))
                        .ReturnsAsync(lecturer);

            // Act
            var result = await _controller.GetLecturer(lecturerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnLecturer = Assert.IsType<LecturerDto>(okResult.Value);
            Assert.Equal(lecturerId, returnLecturer.UserId);
        }

        [Fact]
        public async Task CreateLecturer_ReturnsCreatedAtRouteResult_WithCreatedLecturer()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var lecturerForCreation = new LecturerForCreationDto { StaffId = "12345" };
            var createdLecturer = new LecturerDto { UserId = Guid.NewGuid().ToString(), StaffId = "12345" };
            _mockService.Setup(s => s.LecturerService.CreateLecturer(userId, lecturerForCreation))
                        .ReturnsAsync(createdLecturer);

            // Act
            var result = await _controller.CreateLecturer(userId, lecturerForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnLecturer = Assert.IsType<LecturerDto>(createdAtRouteResult.Value);
            Assert.Equal("GetLecturerByUserId", createdAtRouteResult.RouteName);
            Assert.Equal(createdLecturer.UserId, createdAtRouteResult.RouteValues["userId"]);
        }

        [Fact]
        public async Task UpdateLecturerDetails_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var lecturerForUpdate = new LecturerForUpdateDto
            {
                StaffId = "12345",
                DepartmentId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                ProfileImageUrl = "http://example.com/profile.jpg"
            };

            _mockService.Setup(s => s.LecturerService.UpdateLecturer(userId, lecturerForUpdate))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateLecturerDetails(userId, lecturerForUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
