using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Presentation.Controllers;
using Shared.RequestFeatures;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Test.Controllers
{
    public class CoursesControllerTests
    {
        private readonly Mock<IServiceManager> _mockService;
        private readonly CoursesController _controller;

        public CoursesControllerTests()
        {
            _mockService = new Mock<IServiceManager>();
            _controller = new CoursesController(_mockService.Object);
        }

        [Fact]
        public void GetAllDepartmentCourses_ReturnsOkResult_WithListOfCourses()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var courseParameters = new CourseParameters();
            var courses = new List<CourseDto>
            {
                new CourseDto { Id = Guid.NewGuid(), Title = "Course1" },
                new CourseDto { Id = Guid.NewGuid(), Title = "Course2" }
            };
            var metaData = new MetaData { TotalCount = 2, PageSize = 10, CurrentPage = 1, TotalPages = 1 };
            var pagedResult = (courses, metaData);

            _mockService.Setup(s => s.CourseService.GetDepartmentCourses(departmentId, courseParameters, false))
                        .Returns(pagedResult);

            // Set up HttpContext and Response headers
            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            // Act
            var result = _controller.GetAllDepartmentCourses(departmentId, courseParameters);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCourses = Assert.IsAssignableFrom<IEnumerable<CourseDto>>(okResult.Value);
            Assert.Equal(2, returnCourses.Count());

            // Verify the pagination header was set
            Assert.True(httpContext.Response.Headers.ContainsKey("X-Pagination"));
        }

        [Fact]
        public void GetCourseById_ReturnsOkResult_WithCourse()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();
            var course = new CourseDto { Id = courseId, Title = "Course1" };

            _mockService.Setup(s => s.CourseService.GetDepartmentCourse(departmentId, courseId, false))
                        .Returns(course);

            // Act
            var result = _controller.GetCourseById(departmentId, courseId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCourse = Assert.IsType<CourseDto>(okResult.Value);
            Assert.Equal(courseId, returnCourse.Id);
        }

        [Fact]
        public void CreateCourseForDepartment_ReturnsCreatedAtRouteResult_WithCreatedCourse()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var courseForCreation = new CourseForCreationDto { Title = "Course1" };
            var createdCourse = new CourseDto { Id = Guid.NewGuid(), Title = "Course1" };

            _mockService.Setup(s => s.CourseService.CreateCourseForDepartment(departmentId, courseForCreation, false))
                        .Returns(createdCourse);

            // Act
            var result = _controller.CreateCourseForDepartment(departmentId, courseForCreation);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            var returnCourse = Assert.IsType<CourseDto>(createdAtRouteResult.Value);
            Assert.Equal("GetCourseById", createdAtRouteResult.RouteName);
            Assert.Equal(createdCourse.Id, createdAtRouteResult.RouteValues["id"]);
        }

        [Fact]
        public void DeleteCourseForDepartment_ReturnsNoContentResult()
        {
            // Arrange
            var departmentId = Guid.NewGuid();
            var courseId = Guid.NewGuid();

            _mockService.Setup(s => s.CourseService.DeleteCourseForDepartment(departmentId, courseId))
                        .Callback(() => { });

            // Act
            var result = _controller.DeleteCourseForDepartment(departmentId, courseId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
