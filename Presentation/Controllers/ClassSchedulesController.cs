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
    [Route("api/class-schedules")]
    [ApiController]
    public class ClassSchedulesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ClassSchedulesController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all class schedules
        /// </summary>
        /// <returns>The class schedules list</returns>
        /// <response code="200">Returns the list of class schedules</response>
        [HttpGet(Name = "GetClassSchedules")]
        [ProducesResponseType(typeof(IEnumerable<ClassScheduleDto>), 200)]
        public IActionResult GetClassSchedules()
        {
            var classSchedules = _service.ClassScheduleService.GetClassSchedules(trackChanges: false);
            return Ok(classSchedules);
        }

        /// <summary>
        /// Get a specific class schedule by ID
        /// </summary>
        /// <param name="id">The ID of the class schedule</param>
        /// <returns>The class schedule details</returns>
        /// <response code="200">Returns the class schedule details</response>
        /// <response code="404">If the class schedule is not found</response>
        [HttpGet("{id}", Name = "GetClassScheduleById")]
        [ProducesResponseType(typeof(ClassScheduleDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetClassScheduleById(Guid id)
        {
            var classSchedule = _service.ClassScheduleService.GetClassScheduleById(id, trackChanges: false);
            return Ok(classSchedule);
        }

        /// <summary>
        /// Create a new class schedule
        /// </summary>
        /// <param name="classSchedule">The class schedule data for creation</param>
        /// <returns>The created class schedule</returns>
        /// <response code="201">Returns the newly created class schedule</response>
        /// <response code="400">If the class schedule data is invalid</response>
        [HttpPost(Name = "CreateClassSchedule")]
        [ProducesResponseType(typeof(ClassScheduleDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateClassSchedule([FromBody] ClassScheduleForCreationDto classSchedule)
        {
            var createdClassSchedule = _service.ClassScheduleService.CreateClassSchedule(classSchedule);
            return CreatedAtRoute("GetClassScheduleById", new { id = createdClassSchedule.Id }, createdClassSchedule);
        }

        /// <summary>
        /// Update class schedule by ID
        /// </summary>
        /// <param name="Id">The ID of the class schedule</param>
        /// <param name="classSchedule">The class schedule data for update</param>
        /// <returns>Status code 204 if the class schedule is updated successfully</returns>
        [HttpPut("{Id}", Name = "UpdateClassSchedule")]
        public IActionResult UpdateClassSchedule(Guid Id, [FromBody] ClassScheduleForUpdateDto classSchedule)
        {
            _service.ClassScheduleService.UpdateClassSchedule(Id, classSchedule, trackChanges: true);
            return NoContent();
        }

        /// <summary>
        /// Delete a specific class scheddule by ID
        /// </summary>
        /// <param name="id">The ID for the class schedule to delete</param>
        /// <returns>Empty response</returns>
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteClassSchedule(Guid id)
        {
            _service.ClassScheduleService.DeleteClassSchedule(id);
            return NoContent();
        }
    }
}
