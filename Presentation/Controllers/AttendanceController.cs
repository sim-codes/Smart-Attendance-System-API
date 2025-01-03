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
    [Route("api/attendance/{userId}")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private IServiceManager _service;

        public AttendanceController(IServiceManager service) => _service = service;

        [HttpPost(Name = "CreateAttendance")]
        public async Task<IActionResult> CreateAttendance(string userId, [FromBody] AttendanceForCreationDto attendance)
        {
            var createdAttendance = await _service.AttendanceService.CreateAttendance(userId, attendance);
            return CreatedAtRoute("AttendanceById", new { id = createdAttendance.Id }, createdAttendance);
        }

        [HttpGet("{id:guid}", Name = "AttendanceById")]
        public IActionResult GetAttendance(Guid id)
        {
            var attendance = _service.AttendanceService.GetAttendance(id, trackChanges: false);
            return Ok(attendance);
        }

        [HttpGet(Name = "GetAttendances")]
        public IActionResult GetAttendances()
        {
            var attendances = _service.AttendanceService.GetAttendances(trackChanges: false);
            return Ok(attendances);
        }
    }
}
