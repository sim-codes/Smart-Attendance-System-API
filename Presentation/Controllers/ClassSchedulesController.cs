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

        [HttpGet(Name = "GetClassSchedules")]
        public IActionResult GetClassSchedules()
        {
            var classSchedules = _service.ClassScheduleService.GetClassSchedules(trackChanges: false);
            return Ok(classSchedules);
        }

        [HttpGet("{id}", Name = "GetClassScheduleById")]
        public IActionResult GetClassScheduleById(Guid id)
        {
            var classSchedule = _service.ClassScheduleService.GetClassScheduleById(id, trackChanges: false);
            return Ok(classSchedule);
        }

        [HttpPost(Name = "CreateClassSchedule")]
        public IActionResult CreateClassSchedule([FromBody] ClassScheduleForCreationDto classSchedule)
        {
            var createdClassSchedule = _service.ClassScheduleService.CreateClassSchedule(classSchedule);
            return CreatedAtRoute("GetClassScheduleById", new { id = createdClassSchedule.Id }, createdClassSchedule);
        }
    }
}
