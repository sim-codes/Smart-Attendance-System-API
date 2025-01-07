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
    [Route("api/lecturers")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LecturersController(IServiceManager service) => _service = service;
        /// <summary>
        /// Get the list of all lecturers
        /// </summary>
        /// <param name="lecturerParameters">The lecturer parameters</param>
        /// <returns>The lecturer list</returns>
        /// <response code="200">Returns the list of lectuers</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LecturerDto>), 200)]
        public async Task<IActionResult> GetLecturers([FromQuery] LecturerParameters lecturerParameters)
        {
            var pagedResult = await _service.LecturerService.GetLecturersAsync(lecturerParameters, trackChanges: false);

            Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
            return Ok(pagedResult.lecturers);
        }

        /// <summary>
        /// Get a specific lecturer by User ID
        /// </summary>
        /// <param name="userId">The User ID of the lecturer</param>
        /// <returns>The student details</returns>
        /// <response code="200">Returns the lecturer details</response>
        /// <response code="404">If the lecturer is not found</response>
        [HttpGet("{userId}", Name = "GetLecturerByUserId")]
        [ProducesResponseType(typeof(LecturerDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLecturer(string userId)
        {
            var lecturer = await _service.LecturerService.GetLecturerAsync(userId, trackChanges: false);
            return Ok(lecturer);
        }

        /// <summary>
        /// Create a new lecturer
        /// </summary>
        /// <param name="userId">The User ID of the lecturer</param>
        /// <param name="lecturer">The lecturer data for creation</param>
        /// <returns>The created lecturer</returns>
        /// <response code="201">Returns the newly created lecturer</response>
        /// <response code="400">If the leturer data is invalid</response>
        [HttpPost("{userId}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(LecturerDto), 201)]
        [ProducesResponseType(400)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateStudent(string userId, [FromBody] LecturerForCreationDto lecturer)
        {
            var createdLecturer = await _service.LecturerService.CreateLecturer(userId, lecturer);

            return CreatedAtRoute("GetLecturerByUserId", new { userId = createdLecturer.UserId }, createdLecturer);
        }
    }
}
