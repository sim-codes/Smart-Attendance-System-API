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
        [HttpGet]
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
        [HttpGet("{userId}", Name = "GetStudentByUserId")]
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
        [HttpPost("{userId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStudent(string userId, [FromBody] StudentForCreationDto student)
        {
            if (student is null)
                return BadRequest("StudentForCreationDto object is null");

            student.UserId = userId;

            var createdStudent = await _services.StudentService.CreateStudent(student);

            return CreatedAtRoute("GetStudentByUserId", new { userId = createdStudent.UserId }, createdStudent);
        }
    }
}
