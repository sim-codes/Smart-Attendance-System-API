﻿using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/faculties/{facultyId}/departments")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DepartmentsController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all departments for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <returns>The departments list</returns>
        [HttpGet]
        public IActionResult GetDepartments(Guid facultyId)
        {
            var departments = _service.DepartmentService.GetDepartments(facultyId, trackChanges: false);
            return Ok(departments);
        }

        /// <summary>
        /// Get a specific department by ID for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="id">The ID of the department</param>
        /// <returns>The department details</returns>
        [HttpGet("{id:guid}", Name = "DepartmentById")]
        public IActionResult GetDepartment(Guid facultyId, Guid id)
        {
            var department = _service.DepartmentService.GetDepartment(facultyId, id, trackChanges: false);
            return Ok(department);
        }

        /// <summary>
        /// Create a new department for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="department">The department data for creation</param>
        /// <returns>The created department</returns>
        [HttpPost]
        public IActionResult CreateDepartment(Guid facultyId, [FromBody] DepartmentForCreationDto department)
        {
            if (department is null)
                return BadRequest("DepartmentForCreationDto object is null");

            var createdDepartment = _service.DepartmentService.CreateDepartment(facultyId, department, trackChanges: false);            

            return CreatedAtRoute("DepartmentById", new { facultyId, id = createdDepartment.Id }, createdDepartment);
        }
    }
}
