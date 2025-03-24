using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/students")]
    [ApiController]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IServiceManager _services;

        public StudentsController(IServiceManager serviceManager) => _services = serviceManager;

        /// <summary>
        /// Get the list of all students
        /// </summary>
        /// <param name="studentParameters">The student parameters</param>
        /// <returns>The students list</returns>
        /// <response code="200">Returns the list of students</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentDto>), 200)]
        public async Task<IActionResult> GetAllStudents([FromQuery] StudentParameters studentParameters)
        {
            var pagedResult = await _services.StudentService.GetAllStudentsAsync(studentParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
             
            return Ok(new {Students = pagedResult.students, Metadata = pagedResult.metaData });
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

            var createdStudent = await _services.StudentService.CreateStudent(userId, student);

            return CreatedAtRoute("GetStudentByUserId", new { userId = createdStudent.UserId }, createdStudent);
        }

        /// <summary>
        /// Update student profile by ID
        /// </summary>
        /// <param name="userId">The ID of the student</param>
        /// <param name="student">The class student data for update</param>
        /// <returns>Status code 204 if the student profile is updated successfully</returns>
        /// <response code="204">Returns status code 204 if the student profile is updated successfully</response>
        [HttpPut("{userId}", Name = "UpdateStudentDetail")]
        [ProducesResponseType(204)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> UpdateStudentDetails(string userId, [FromBody] StudentForUpdateDto student)
        {
            await _services.StudentService.UpdateStudent(userId, student);
            return NoContent();
        }
    }
}
