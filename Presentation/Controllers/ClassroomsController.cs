using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/faculties/{facultyId}/classrooms")]
    [ApiController]
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
    }
}
