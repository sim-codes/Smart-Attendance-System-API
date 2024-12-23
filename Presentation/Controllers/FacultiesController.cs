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
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateFaculty([FromBody] FacultyForCreationDto faculty)
        {
            var createdFaculty = _service.FacultyService.CreateFaculty(faculty);
            return CreatedAtRoute("FacultyById", new { id = createdFaculty.Id }, createdFaculty);
        }
    }
}
