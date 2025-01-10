using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Presentation.Controllers;

namespace Test.Controllers
{
    public class ClassSchedulesControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly ClassSchedulesController _controller;

        public ClassSchedulesControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new ClassSchedulesController(_mockService.Object);
        }

        [Fact]
        public void GetClassSchedules_ReturnsOkResult_WithListOfClassSchedules()
        {
            // Arrange
            var classSchedules = new List<ClassScheduleDto>
            {
                new ClassScheduleDto { Id = Guid.NewGuid(), DayOfWeek = "Monday" },
                new ClassScheduleDto { Id = Guid.NewGuid(), DayOfWeek = "Tuesday" }
            };

            _mockService.Setup(s => s.ClassScheduleService.GetClassSchedules(false))
                        .Returns(classSchedules);

            // Act
            var result = _controller.GetClassSchedules();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnClassSchedules = Assert.IsAssignableFrom<IEnumerable<ClassScheduleDto>>(okResult.Value);
            Assert.Equal(2, returnClassSchedules.Count());
        }

        [Fact]
        public void GetClassScheduleById_ReturnsOkResult_WithClassSchedule()
        {
            // Arrange
            var classScheduleId = Guid.NewGuid();
            var classSchedule = new ClassScheduleDto { Id = classScheduleId, DayOfWeek = "Monday" };
            _mockService.Setup(s => s.ClassScheduleService.GetClassScheduleById(classScheduleId, false))
                        .Returns(classSchedule);

            // Act
            var result = _controller.GetClassScheduleById(classScheduleId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnClassSchedule = Assert.IsType<ClassScheduleDto>(okResult.Value);
            Assert.Equal(classScheduleId, returnClassSchedule.Id);
        }

        [Fact]
        public void CreateClassSchedule_ReturnsCreatedAtRouteResult_WithCreatedClassSchedule()
        {
            // Arrange
            var classScheduleForCreation = new ClassScheduleForCreationDto { DayOfWeek = "Monday" };
            var createdClassSchedule = new ClassScheduleDto { Id = Guid.NewGuid(), DayOfWeek = "Monday" };
            _mockService.Setup(s => s.ClassScheduleService.CreateClassSchedule(classScheduleForCreation))
                        .Returns(createdClassSchedule);

            // Act
            var result = _controller.CreateClassSchedule(classScheduleForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnClassSchedule = Assert.IsType<ClassScheduleDto>(createdAtRouteResult.Value);
            Assert.Equal("GetClassScheduleById", createdAtRouteResult.RouteName);
            Assert.Equal(createdClassSchedule.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public void UpdateClassSchedule_ReturnsNoContentResult()
        {
            // Arrange
            var classScheduleId = Guid.NewGuid();
            var classScheduleForUpdate = new ClassScheduleForUpdateDto
            {
                DayOfWeek = "Monday",
                StartTime = new TimeOnly(9, 0),
                EndTime = new TimeOnly(10, 0),
                SessionId = "2023",
                CourseId = Guid.NewGuid(),
                ClassroomId = Guid.NewGuid()
            };

            _mockService.Setup(s => s.ClassScheduleService.UpdateClassSchedule(classScheduleId, classScheduleForUpdate, true))
                        .Callback(() => { });

            // Act
            var result = _controller.UpdateClassSchedule(classScheduleId, classScheduleForUpdate);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void DeleteClassSchedule_ReturnsNoContentResult()
        {
            // Arrange
            var classScheduleId = Guid.NewGuid();

            _mockService.Setup(s => s.ClassScheduleService.DeleteClassSchedule(classScheduleId))
                        .Callback(() => { }); // Mocking a void method

            // Act
            var result = _controller.DeleteClassSchedule(classScheduleId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}
