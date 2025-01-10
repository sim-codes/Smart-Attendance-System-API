using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Presentation.Controllers;

namespace Tests
{
    public class ClassroomsControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly ClassroomsController _controller;

        public ClassroomsControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new ClassroomsController(_mockService.Object);
        }

        [Fact]
        public void GetClassrooms_ReturnsOkResult_WithListOfClassrooms()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var classrooms = new List<ClassroomDto>
            {
                new ClassroomDto { Id = Guid.NewGuid(), Name = "Classroom1" },
                new ClassroomDto { Id = Guid.NewGuid(), Name = "Classroom2" }
            };

            _mockService.Setup(s => s.ClassroomService.GetClassrooms(facultyId, false))
                        .Returns(classrooms);

            // Act
            var result = _controller.GetClassrooms(facultyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnClassrooms = Assert.IsAssignableFrom<IEnumerable<ClassroomDto>>(okResult.Value);
            Assert.Equal(2, returnClassrooms.Count());
        }

        [Fact]
        public void GetClassroom_ReturnsOkResult_WithClassroom()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var classroomId = Guid.NewGuid();
            var classroom = new ClassroomDto { Id = classroomId, Name = "Classroom1" };

            _mockService.Setup(s => s.ClassroomService.GetClassroom(facultyId, classroomId, false))
                        .Returns(classroom);

            // Act
            var result = _controller.GetClassroom(facultyId, classroomId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnClassroom = Assert.IsType<ClassroomDto>(okResult.Value);
            Assert.Equal(classroomId, returnClassroom.Id);
        }

        [Fact]
        public void CreateClassroom_ReturnsCreatedAtRouteResult_WithCreatedClassroom()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var classroomForCreation = new ClassroomForCreationDto
            {
                Name = "Classroom1",
                TopLeftLat = 0,
                TopLeftLon = 0,
                TopRightLat = 0,
                TopRightLon = 0,
                BottomLeftLat = 0,
                BottomLeftLon = 0,
                BottomRightLat = 0,
                BottomRightLon = 0
            };
            var createdClassroom = new ClassroomDto { Id = Guid.NewGuid(), Name = "Classroom1" };

            _mockService.Setup(s => s.ClassroomService.CreateClassroom(facultyId, classroomForCreation, false))
                        .Returns(createdClassroom);

            // Act
            var result = _controller.CreateClassroom(facultyId, classroomForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnClassroom = Assert.IsType<ClassroomDto>(createdAtRouteResult.Value);
            Assert.Equal("ClassroomById", createdAtRouteResult.RouteName);
            Assert.Equal(createdClassroom.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public void DeleteClassroom_ReturnsNoContentResult()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var classroomId = Guid.NewGuid();

            _mockService.Setup(s => s.ClassroomService.DeleteClassroomForFaculty(facultyId, classroomId, false))
                        .Callback(() => { });

            // Act
            var result = _controller.DeleteClassroom(facultyId, classroomId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
