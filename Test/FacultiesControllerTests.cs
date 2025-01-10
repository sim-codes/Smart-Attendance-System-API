using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Presentation.Controllers;

namespace Tests
{
    public class FacultiesControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly FacultiesController _controller;

        public FacultiesControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new FacultiesController(_mockService.Object);
        }

        [Fact]
        public void GetFaculties_ReturnsOkResult_WithListOfFaculties()
        {
            // Arrange
            var faculties = new List<FacultyDto>
            {
                new FacultyDto { Id = Guid.NewGuid(), Name = "Faculty1" },
                new FacultyDto { Id = Guid.NewGuid(), Name = "Faculty2" }
            };

            _mockService.Setup(s => s.FacultyService.GetAllFaculties(false))
                        .Returns(faculties);

            // Act
            var result = _controller.GetFaculties();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnFaculties = Assert.IsAssignableFrom<IEnumerable<FacultyDto>>(okResult.Value);
            Assert.Equal(2, returnFaculties.Count());
        }

        [Fact]
        public void GetFaculty_ReturnsOkResult_WithFaculty()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var faculty = new FacultyDto { Id = facultyId, Name = "Faculty1" };

            _mockService.Setup(s => s.FacultyService.GetFaculty(facultyId, false))
                        .Returns(faculty);

            // Act
            var result = _controller.GetFaculty(facultyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnFaculty = Assert.IsType<FacultyDto>(okResult.Value);
            Assert.Equal(facultyId, returnFaculty.Id);
        }

        [Fact]
        public void CreateFaculty_ReturnsCreatedAtRouteResult_WithCreatedFaculty()
        {
            // Arrange
            var facultyForCreation = new FacultyForCreationDto { Name = "Faculty1" };
            var createdFaculty = new FacultyDto { Id = Guid.NewGuid(), Name = "Faculty1" };

            _mockService.Setup(s => s.FacultyService.CreateFaculty(facultyForCreation))
                        .Returns(createdFaculty);

            // Act
            var result = _controller.CreateFaculty(facultyForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnFaculty = Assert.IsType<FacultyDto>(createdAtRouteResult.Value);
            Assert.Equal("FacultyById", createdAtRouteResult.RouteName);
            Assert.Equal(createdFaculty.Id, createdAtRouteResult.RouteValues["id"]);
        }
    }
}
