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

namespace Test.Controllers
{
    public class StudentsControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly StudentsController _controller;

        public StudentsControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new StudentsController(_mockService.Object);
        }

        [Fact]
        public async Task GetAllStudents_ReturnsOkResult_WithListOfStudents()
        {
            // Arrange
            var studentParameters = new StudentParameters
            {
                PageNumber = 1,
                PageSize = 10,
                SearchTerm = null
            };

            var students = new List<StudentDto>
            {
                new StudentDto { UserId = Guid.NewGuid().ToString(), MatriculationNumber = "12345" },
                new StudentDto { UserId = Guid.NewGuid().ToString(), MatriculationNumber = "67890" }
            };

            var metaData = new MetaData
            {
                CurrentPage = studentParameters.PageNumber,
                PageSize = studentParameters.PageSize,
                TotalCount = students.Count,
                TotalPages = 1
            };

            _mockService.Setup(s => s.StudentService.GetAllStudentsAsync(studentParameters, false))
                        .ReturnsAsync((students, metaData));

            // Set up HttpContext and Response headers
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = await _controller.GetAllStudents(studentParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudents = Assert.IsAssignableFrom<IEnumerable<StudentDto>>(okResult.Value);
            Assert.Equal(2, returnStudents.Count());

            // Verify the pagination header was set
            Assert.True(httpContext.Response.Headers.ContainsKey("X-Pagination"));
        }

        [Fact]
        public async Task GetStudent_ReturnsOkResult_WithStudent()
        {
            // Arrange
            var studentId = Guid.NewGuid().ToString();
            var student = new StudentDto { UserId = studentId, MatriculationNumber = "12345" };
            _mockService.Setup(s => s.StudentService.GetStudentAsync(studentId, false))
                        .ReturnsAsync(student);

            // Act
            var result = await _controller.GetStudent(studentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnStudent = Assert.IsType<StudentDto>(okResult.Value);
            Assert.Equal(studentId, returnStudent.UserId);
        }

        [Fact]
        public async Task CreateStudent_ReturnsCreatedAtRouteResult_WithCreatedStudent()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var studentForCreation = new StudentForCreationDto { MatriculationNumber = "12345" };
            var createdStudent = new StudentDto { UserId = Guid.NewGuid().ToString(), MatriculationNumber = "12345" };
            _mockService.Setup(s => s.StudentService.CreateStudent(userId, studentForCreation))
                        .ReturnsAsync(createdStudent);

            // Act
            var result = await _controller.CreateStudent(userId, studentForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnStudent = Assert.IsType<StudentDto>(createdAtRouteResult.Value);
            Assert.Equal("GetStudentByUserId", createdAtRouteResult.RouteName);
            Assert.Equal(createdStudent.UserId, createdAtRouteResult.RouteValues["userId"]);
        }

        [Fact]
        public async Task UpdateStudentDetails_ReturnsNoContentResult()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var studentForUpdate = new StudentForUpdateDto
            {
                MatriculationNumber = "12345",
                LevelId = Guid.NewGuid(),
                DepartmentId = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                PhoneNumber = "123-456-7890",
                ProfileImageUrl = "http://example.com/profile.jpg"
            };

            _mockService.Setup(s => s.StudentService.UpdateStudent(userId, studentForUpdate))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateStudentDetails(userId, studentForUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
