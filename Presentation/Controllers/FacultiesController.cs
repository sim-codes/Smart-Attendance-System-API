using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly IServiceManager _service;

        public FacultiesController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all faculties
        /// </summary>
        /// <returns>The faculties list</returns>
        [HttpGet(Name = "GetFaculties")]
        public IActionResult GetFaculties()
        {
            var faculties = _service.FacultyService.GetAllFaculties(trackChanges: false);
            return Ok(faculties);
        }

        /// <summary>
        /// Get a specific faculty by ID
        /// </summary>
        /// <param name="id">The ID of the faculty</param>
        /// <returns>The faculty details</returns>
        [HttpGet("{id:guid}", Name = "FacultyById")]
        public IActionResult GetFaculty(Guid id)
        {
            var faculty = _service.FacultyService.GetFaculty(id, trackChanges: false);
            return Ok(faculty);
        }

        /// <summary>
        /// Create a new faculty
        /// </summary>
        /// <param name="faculty">The faculty data for creation</param>
        /// <returns>The created faculty</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        /// <response code="422">If the model is invalid</response>
        [HttpPost(Name = "CreateFaculty")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateFaculty([FromBody] FacultyForCreationDto faculty)
        {
            var createdFaculty = _service.FacultyService.CreateFaculty(faculty);
            return CreatedAtRoute("FacultyById", new { id = createdFaculty.Id }, createdFaculty);
        }
    }
}
