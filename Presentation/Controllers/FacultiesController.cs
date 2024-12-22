using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public IActionResult GetFaculties()
        {
            var faculties = _service.FacultyService.GetAllFaculties(trackChanges: false);
            return Ok(faculties);
        }

        [HttpGet("{id:guid}", Name = "FacultyById")]
        public IActionResult GetFaculty(Guid id)
        {
            var faculty = _service.FacultyService.GetFaculty(id, trackChanges: false);
            return Ok(faculty);
        }

        [HttpPost]
        public IActionResult CreateFaculty([FromBody] FacultyForCreationDto faculty)
        {
            if (faculty is null)
                return BadRequest("FacultyForCreationDto object is null");

            var createdFaculty = _service.FacultyService.CreateFaculty(faculty);
            return CreatedAtRoute("FacultyById", new { id = createdFaculty.Id }, createdFaculty);
        }
    }
}
