using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IServiceManager _service;

        public ProfileController(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _service.ProfileService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _service.ProfileService.GetUserById(id);
            return Ok(user);
        }
    }
}
