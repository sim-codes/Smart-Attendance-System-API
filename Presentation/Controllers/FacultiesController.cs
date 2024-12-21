using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                var faculties = _service.FacultyService.GetAllFaculties(trackChanges: false);
                return Ok(faculties);
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
