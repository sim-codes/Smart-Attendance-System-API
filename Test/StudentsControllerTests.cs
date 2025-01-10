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

namespace Tests
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
    }
}
