﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateFaculty([FromBody] FacultyForCreationDto faculty)
        {
            var createdFaculty = _service.FacultyService.CreateFaculty(faculty);
            return CreatedAtRoute("FacultyById", new { id = createdFaculty.Id }, createdFaculty);
        }
    }
}
