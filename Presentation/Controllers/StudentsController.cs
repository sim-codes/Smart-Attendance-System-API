using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public StudentsController(IServiceManager serviceManager) => _services = serviceManager;

        /// <summary>
        /// Get the list of all students
        /// </summary>
        /// <returns>The students list</returns>
        /// <response code="200">Returns the list of students</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _services.StudentService.GetAllStudentsAsync(trackChanges: false);
            return Ok(students);
        }

        /// <summary>
        /// Get a specific student by User ID
        /// </summary>
        /// <param name="userId">The User ID of the student</param>
        /// <returns>The student details</returns>
        /// <response code="200">Returns the student details</response>
        /// <response code="404">If the student is not found</response>
        [HttpGet("{userId}", Name = "GetStudentByUserId")]
        [ProducesResponseType(typeof(StudentDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetStudent(string userId)
        {
            var student = await _services.StudentService.GetStudentAsync(userId, trackChanges: false);
            return Ok(student);
        }

        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="userId">The User ID of the student</param>
        /// <param name="student">The student data for creation</param>
        /// <returns>The created student</returns>
        /// <response code="201">Returns the newly created student</response>
        /// <response code="400">If the student data is invalid</response>
        [HttpPost("{userId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(StudentDto), 201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStudent(string userId, [FromBody] StudentForCreationDto student)
        {
            student.UserId = userId;

            var createdStudent = await _services.StudentService.CreateStudent(student);

            return CreatedAtRoute("GetStudentByUserId", new { userId = createdStudent.UserId }, createdStudent);
        }
    }
}
