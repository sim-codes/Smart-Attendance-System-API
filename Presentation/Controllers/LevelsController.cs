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
    [Route("api/levels")]
    [ApiController]
    public class LevelsController : ControllerBase
    {
        private readonly IServiceManager _service;

        public LevelsController(IServiceManager service) => _service = service;

        /// <summary>
        /// Get the list of all levels
        /// </summary>
        /// <returns>The levels list</returns>
        /// <response code="200">Returns the list of levels</response>
        [HttpGet(Name = "GetAllLevels")]
        [ProducesResponseType(typeof(IEnumerable<LevelDto>), 200)]
        public IActionResult GetAllLevels()
        {
            var levels = _service.LevelService.GetAllLevels(trackChanges: false);
            return Ok(levels);
        }

        /// <summary>
        /// Get a specific level by ID
        /// </summary>
        /// <param name="id">The ID of the level</param>
        /// <returns>The level details</returns>
        /// <response code="200">Returns the level details</response>
        /// <response code="404">If the level is not found</response>
        [HttpGet("{id:guid}", Name = "GetLevelById")]
        [ProducesResponseType(typeof(LevelDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetLevelById(Guid id)
        {
            var level = _service.LevelService.GetLevel(id, trackChanges: false);
            return Ok(level);
        }

        /// <summary>
        /// Create a new level
        /// </summary>
        /// <param name="level">The level data for creation</param>
        /// <returns>The created level</returns>
        /// <response code="201">Returns the newly created level</response>
        /// <response code="400">If the level data is invalid</response>
        [HttpPost(Name = "CreateLevel")]
        [ProducesResponseType(typeof(LevelDto), 201)]
        [ProducesResponseType(400)]
        public IActionResult CreateLevel([FromBody] LevelForCreationDto level)
        {
            var createdLevel = _service.LevelService.CreateLevel(level);
            return CreatedAtRoute("GetLevelById", new { id = createdLevel.Id }, createdLevel);
        }
    }
}
