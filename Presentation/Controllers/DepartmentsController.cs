using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public DepartmentsController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all departments for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <returns>The departments list</returns>
        [HttpGet("faculties/{facultyId}/departments")]
        public IActionResult GetDepartmentsForFaculty(Guid facultyId)
        {
            var departments = _service.DepartmentService.GetDepartmentsForFaculty(facultyId, trackChanges: false);
            return Ok(departments);
        }

        /// <summary>
        /// Get the list of all departments
        /// </summary>
        /// <returns>The departments list</returns>
        [HttpGet("departments")]
        public IActionResult GetAllDepartments()
        {
            var departments = _service.DepartmentService.GetAllDepartments(trackChanges: false);
            return Ok(departments);
        }

        /// <summary>
        /// Get a specific department by ID for a specific faculty
        /// </summary>
        /// <param name="facultyId">The ID of the faculty</param>
        /// <param name="id">The ID of the department</param>
        /// <returns>The department details</returns>
        [HttpGet("faculties/{facultyId}/departments/{id:guid}", Name = "DepartmentById")]
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
        [HttpPost("faculties/{facultyId}/departments")]
        public IActionResult CreateDepartment(Guid facultyId, [FromBody] DepartmentForCreationDto department)
        {
            var createdDepartment = _service.DepartmentService.CreateDepartment(facultyId, department, trackChanges: false);            

            return CreatedAtRoute("DepartmentById", new { facultyId, id = createdDepartment.Id }, createdDepartment);
        }
    }
}
