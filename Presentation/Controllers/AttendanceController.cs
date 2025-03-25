using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    [Route("api/attendance")]
    [ApiController]
    [Authorize]
    public class AttendanceController : ControllerBase
    {
        private IServiceManager _service;

        public AttendanceController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all attendance records
        /// </summary>
        /// /// <param name="attendanceParameters">Attendace parameters for filtering and searching</param>
        /// <returns>The attendance records list</returns>
        /// <response code="200">Returns the list of attendance records</response>
        [HttpGet(Name = "GetAttendanceRecords")]
        [ProducesResponseType(typeof(IEnumerable<AttendanceDto>), 200)]
        public IActionResult GetAllAttendanceRecords([FromQuery] AttendanceParameters attendanceParameters)
        {
            var pageResult = _service.AttendanceService.GetAttendanceRecords(attendanceParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageResult.metaData));

            return Ok(new {Reports = pageResult.attendanceRecords, Metadata = pageResult.metaData});
        }

        /// <summary>
        /// Create a new attendance record
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="attendance">The attendance data for creation</param>
        /// <returns>The created attendance record</returns>
        /// <response code="201">Returns the newly created attendance record</response>
        /// <response code="400">If the attendance data is invalid</response>
        /// <response code="404">If the classroom is not found</response>
        /// <response code="409">If the student is not in the classroom</response>
        [HttpPost("{userId}", Name = "CreateAttendance")]
        [ProducesResponseType(typeof(AttendanceDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> CreateAttendance(string userId, [FromBody] AttendanceForCreationDto attendance)
        {
            var createdAttendance = await _service.AttendanceService.CreateAttendance(userId, attendance);
            return CreatedAtRoute("AttendanceById", new { id = createdAttendance.Id }, createdAttendance);
        }

        /// <summary>
        /// Create a new attendance record without verifying location
        /// </summary>
        /// <param name="userId">The ID of the user</param>
        /// <param name="attendance">The attendance data for creation</param>
        /// <returns>The created attendance record</returns>
        /// <response code="201">Returns the newly created attendance record</response>
        /// <response code="400">If the attendance data is invalid</response>
        [HttpPost("{userId}/signin", Name = "CreateAttendanceWithoutLocation")]
        [ProducesResponseType(typeof(AttendanceDto), 201)]
        [ProducesResponseType(400)]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> SignAttendanceWithoutLocation(string userId, [FromBody] AttendanceForCreationDto attendance)
        {
            var createdAttendance = await _service.AttendanceService.SignAttendanceWithoutLocation(userId, attendance);
            Console.WriteLine(createdAttendance);
            return Ok(createdAttendance);
        }

        /// <summary>
        /// Get a specific attendance record by ID
        /// </summary>
        /// <param name="id">The ID of the attendance record</param>
        /// <returns>The attendance record details</returns>
        /// <response code="200">Returns the attendance record details</response>
        /// <response code="404">If the attendance record is not found</response>
        [HttpGet("{id:guid}", Name = "AttendanceById")]
        [ProducesResponseType(typeof(AttendanceDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetAttendance(Guid id)
        {
            var attendance = _service.AttendanceService.GetAttendance(id, trackChanges: false);
            return Ok(attendance);
        }
    }
}
