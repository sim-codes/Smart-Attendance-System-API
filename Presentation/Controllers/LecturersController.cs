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
    [Route("api/lecturers")]
    [ApiController]
    public class LecturersController : ControllerBase
    {
        private readonly IServiceManager _service;
        public LecturersController(IServiceManager service) => _service = service;
        /// <summary>
        /// Get the list of all lecturers
        /// </summary>
        /// <returns>The lecturer list</returns>
        /// <response code="200">Returns the list of lectuers</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LecturerDto>), 200)]
        public async Task<IActionResult> GetLecturers()
        {
            var lecturers = await _service.LecturerService.GetLecturersAsync(trackChanges: false);
            return Ok(lecturers);
        }

        /// <summary>
        /// Get a specific lecturer by User ID
        /// </summary>
        /// <param name="userId">The User ID of the lecturer</param>
        /// <returns>The student details</returns>
        /// <response code="200">Returns the lecturer details</response>
        /// <response code="404">If the lecturer is not found</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(LecturerDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetLecturer(string userId)
        {
            var lecturer = await _service.LecturerService.GetLecturerAsync(userId, trackChanges: false);
            return Ok(lecturer);
        }


    }
}
