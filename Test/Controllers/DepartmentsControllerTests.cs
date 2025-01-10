using Microsoft.AspNetCore.Mvc;
using Moq;
using Presentation.Controllers;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Test.Controllers
{
    public class DepartmentsControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly DepartmentsController _controller;

        public DepartmentsControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new DepartmentsController(_mockService.Object);
        }

        [Fact]
        public void GetDepartments_ReturnsOkResult_WithListOfDepartments()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var departments = new List<DepartmentDto>
            {
                new DepartmentDto { Id = Guid.NewGuid(), Name = "Department1" },
                new DepartmentDto { Id = Guid.NewGuid(), Name = "Department2" }
            };

            _mockService.Setup(s => s.DepartmentService.GetDepartments(facultyId, false))
                        .Returns(departments);

            // Act
            var result = _controller.GetDepartments(facultyId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDepartments = Assert.IsAssignableFrom<IEnumerable<DepartmentDto>>(okResult.Value);
            Assert.Equal(2, returnDepartments.Count());
        }

        [Fact]
        public void GetDepartment_ReturnsOkResult_WithDepartment()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var departmentId = Guid.NewGuid();
            var department = new DepartmentDto { Id = departmentId, Name = "Department1" };

            _mockService.Setup(s => s.DepartmentService.GetDepartment(facultyId, departmentId, false))
                        .Returns(department);

            // Act
            var result = _controller.GetDepartment(facultyId, departmentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnDepartment = Assert.IsType<DepartmentDto>(okResult.Value);
            Assert.Equal(departmentId, returnDepartment.Id);
        }

        [Fact]
        public void CreateDepartment_ReturnsCreatedAtRouteResult_WithCreatedDepartment()
        {
            // Arrange
            var facultyId = Guid.NewGuid();
            var departmentForCreation = new DepartmentForCreationDto { Name = "Department1" };
            var createdDepartment = new DepartmentDto { Id = Guid.NewGuid(), Name = "Department1" };

            _mockService.Setup(s => s.DepartmentService.CreateDepartment(facultyId, departmentForCreation, false))
                        .Returns(createdDepartment);

            // Act
            var result = _controller.CreateDepartment(facultyId, departmentForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnDepartment = Assert.IsType<DepartmentDto>(createdAtRouteResult.Value);
            Assert.Equal("DepartmentById", createdAtRouteResult.RouteName);
            Assert.Equal(createdDepartment.Id, createdAtRouteResult.RouteValues["id"]);
        }
    }
}
