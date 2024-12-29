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
    [Route("api/academic-sessions")]
    [ApiController]
    public class AcademicSessionsController : ControllerBase
    {
        private IServiceManager _service;

        public AcademicSessionsController(IServiceManager service) => _service = service;

        [HttpGet(Name = "GetAcademicSessions")]
        public IActionResult GetAcademicSessions()
        {
            var academicSessions = _service.AcademicSessionService.GetAllAcademicSessions(trackChanges: false);
            return Ok(academicSessions);
        }

        /// <summary>
        /// Get a specific academic session by ID
        /// </summary>
        /// <param name="id">The ID of the academic session</param>
        /// <returns>The academic session details</returns>
        /// <response code="200">Returns the academic session details</response>
        /// <response code="404">If the academic session is not found</response>
        [HttpGet("{id:guid}", Name = "AcademicSessionById")]
        [ProducesResponseType(typeof(AcademicSessionDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetAcademicSession(Guid id)
        {
            var academicSession = _service.AcademicSessionService.GetAcademicSession(id, trackChanges: false);
            return Ok(academicSession);
        }

        /// <summary>
        /// Create a new academic session
        /// </summary>
        /// <param name="academicSession">The academic session data for creation</param>
        /// <returns>The created academic session</returns>
        /// <response code="201">Returns the newly created academic session</response>
        /// <response code="400">If the academic session data is invalid</response>
        [HttpPost(Name = "CreateAcademicSession")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ProducesResponseType(typeof(AcademicSessionDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateAcademicSession([FromBody] AcademicSessionForCreationDto academicSession)
        {
            var createdAcademicSession = _service.AcademicSessionService.CreateAcademicSession(academicSession);
            return CreatedAtRoute("AcademicSessionById", new { id = createdAcademicSession.Id }, createdAcademicSession);
        }

        /// <summary>
        /// Delete an academic session by ID
        /// </summary>
        /// <param name="id">The ID of the academic session to delete</param>
        /// <response code="204">If the academic session is successfully deleted</response>
        /// <response code="404">If the academic session is not found</response>
        [HttpDelete("{id:guid}", Name = "DeleteAcademicSession")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAcademicSession(Guid id)
        {
            _service.AcademicSessionService.DeleteAcademicSession(id, trackChanges: false);
            return NoContent();
        }

    }
}
