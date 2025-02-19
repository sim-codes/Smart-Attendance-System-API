using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/enrollments/{userId}")]
    [ApiController]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private IServiceManager _service;

        public EnrollmentsController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all courses enrolled by a specific student
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <returns>The list of enrolled courses</returns>
        [HttpGet(Name = "GetEnrollments")]
        [ProducesResponseType(typeof(IEnumerable<EnrollmentDto>), 200)]
        public IActionResult GetEnrollments(string userId)
        {
            var enrollments = _service.EnrollmentService.GetAllCoursesEnrolledByStudent(userId, trackChanges: false);
            return Ok(enrollments);
        }

        /// <summary>
        /// Get a specific enrolled course by ID for a specific student
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <param name="courseId">The ID of the course</param>
        /// <returns>The enrolled course details</returns>
        [HttpGet("{courseId:guid}", Name = "GetEnrolledCourseById")]
        [ProducesResponseType(typeof(EnrollmentDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetEnrolledCourseById(string userId, Guid courseId)
        {
            var enrollment = _service.EnrollmentService.GetCourseEnrolledByStudent(userId, courseId, trackChanges: false);
            if (enrollment is null)
                return NotFound();

            var students = _service.EnrollmentService.GetStudentsEnrolledByCourse(courseId, false);

            return Ok(new { Students = students, Enrollment = enrollment});
        }

        /// <summary>
        /// Enroll a student for a course
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <param name="enrollmentForCreation">The enrollment data for creation</param>
        /// <returns>The created enrollment</returns>
        [HttpPost(Name = "EnrollForCourse")]
        [ProducesResponseType(typeof(EnrollmentDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult EnrollForCourse(string userId, [FromBody] EnrollmentForCreationDto enrollmentForCreation)
        {
            if (enrollmentForCreation == null)
                return BadRequest("EnrollmentForCreationDto object is null");

            var createdEnrollment = _service.EnrollmentService.EnrollStudentForCourse(userId, enrollmentForCreation, trackChanges: false);

            return CreatedAtRoute("GetEnrolledCourseById", new { userId, courseId = createdEnrollment.CourseId }, createdEnrollment);
        }

        /// <summary>
        /// Delete a specific enrolled course by ID for a specific student
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <param name="courseId">The ID of the course</param>
        /// <returns>Empty response</returns>
        [HttpDelete("{courseId:guid}", Name = "DeleteEnrolledCourse")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEnrolledCourse(string userId, Guid courseId)
        {
            var enrollment = _service.EnrollmentService.GetCourseEnrolledByStudent(userId, courseId, trackChanges: false);
            if (enrollment == null)
                return NotFound();

            _service.EnrollmentService.DeleteCourseEnrolledByStudent(userId, courseId, trackChanges: false);
            return NoContent();
        }
    }
}
