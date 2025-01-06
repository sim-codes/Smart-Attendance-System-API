using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/departments/{departmentId}/courses")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public CoursesController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all courses for a specific department
        /// </summary>
        /// <param name="departmentId">The ID of the department</param>
        /// <param name="courseParameters">The course parameters</param>
        /// <returns>The courses list</returns>
        /// <response code="200">Returns the list of courses</response>
        [HttpGet(Name = "GetAllDepartmentCourses")]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), 200)]
        public IActionResult GetAllDepartmentCourses(Guid departmentId, [FromQuery] CourseParameters courseParameters)
        {
            var courses = _service.CourseService.GetDepartmentCourses(departmentId, courseParameters, trackChanges: false);
            return Ok(courses);
        }

        /// <summary>
        /// Get a specific course by ID for a specific department
        /// </summary>
        /// <param name="departmentId">The ID of the department</param>
        /// <param name="id">The ID of the course</param>
        /// <returns>The course details</returns>
        /// <response code="200">Returns the course details</response>
        /// <response code="404">If the course is not found</response>
        [HttpGet("{id:guid}", Name = "GetCourseById")]
        [ProducesResponseType(typeof(CourseDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetCourseById(Guid departmentId, Guid id)
        {
            var course = _service.CourseService.GetDepartmentCourse(departmentId, id, trackChanges: false);
            return Ok(course);
        }

        /// <summary>
        /// Create a new course for a specific department
        /// </summary>
        /// <param name="departmentId">The ID of the department</param>
        /// <param name="course">The course data for creation</param>
        /// <returns>The created course</returns>
        /// <response code="201">Returns the newly created course</response>
        /// <response code="400">If the course data is invalid</response>
        [HttpPost(Name = "CreateCourseForDepartment")]
        [ProducesResponseType(typeof(CourseDto), 201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateCourseForDepartment(Guid departmentId, [FromBody] CourseForCreationDto course)
        {
            if (course == null)
                return BadRequest("CourseForCreationDto object is null");

            var createdCourse = _service.CourseService.CreateCourseForDepartment(departmentId, course, trackChanges: false);
            return CreatedAtRoute("GetCourseById", new { departmentId, id = createdCourse.Id }, createdCourse);
        }

        /// <summary>
        /// Delete a specific course by ID
        /// </summary>
        /// <param name="departmentId">The ID for the department to delete course for</param>
        /// <param name="id">The ID for the course to delete</param>
        /// <returns>Empty response</returns>
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCourseForDepartment(Guid departmentId, Guid id)
        {
            _service.CourseService.DeleteCourseForDepartment(departmentId, id);
            return NoContent();
        }
    }
}
