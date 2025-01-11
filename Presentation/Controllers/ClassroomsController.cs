using Microsoft.AspNetCore.Authorization;
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
    [Route("api/faculties/{facultyId}/classrooms")]
    [ApiController]
    [Authorize]
    public class ClassroomsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ClassroomsController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all classrooms for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <returns>The classroom list</returns>
        [HttpGet]
        public IActionResult GetClassrooms(Guid facultyId)
        {
            var classrooms = _service.ClassroomService.GetClassrooms(facultyId, trackChanges: false);
            return Ok(classrooms);
        }


        /// <summary>
        /// Get a specific classroom by ID for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="id">The ID of the classroom</param>
        /// <returns>The classroom details</returns>
        [HttpGet("{id:guid}", Name = "ClassroomById")]
        public IActionResult GetClassroom(Guid facultyId, Guid id)
        {
            var classroom = _service.ClassroomService.GetClassroom(facultyId, id, trackChanges: false);
            return Ok(classroom);
        }

        /// <summary>
        /// Create a new classroom for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="classroom">The classroom data for creation</param>
        /// <returns>The created classroom</returns>
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateClassroom(Guid facultyId, [FromBody] ClassroomForCreationDto classroom)
        {
            if (classroom is null)
                return BadRequest("DepartmentForCreationDto object is null");

            var createdClassroom = _service.ClassroomService.CreateClassroom(facultyId, classroom, trackChanges: false);

            return CreatedAtRoute("ClassroomById", new { facultyId, id = createdClassroom.Id }, createdClassroom);
        }

        /// <summary>
        /// Delete a specific classroom by ID for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="id">The ID for the classroom to delete</param>
        /// <returns>Empty response</returns>
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteClassroom(Guid facultyId, Guid id)
        {
            _service.ClassroomService.DeleteClassroomForFaculty(facultyId, id, trackChanges: false);
            return NoContent();
        }
    }
}
