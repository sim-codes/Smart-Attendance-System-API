using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class ProfileService : IProfileService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly JwtConfiguration _jwtConfiguration;

        private User? _user;

        public ProfileService(ILoggerManager logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _jwtConfiguration = new JwtConfiguration();
            _configuration.Bind(_jwtConfiguration.Section, _jwtConfiguration);
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var userFromDb = await _userManager.FindByIdAsync(id.ToString());
            if (userFromDb is null)
                throw new UserNotFoundException(id.ToString());

            var userDto = _mapper.Map<UserDto>(userFromDb);
            var roles = await _userManager.GetRolesAsync(userFromDb);
            return userDto with { Roles = [.. roles] };
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var mappedUser = _mapper.Map<UserDto>(user);
                var roles = await _userManager.GetRolesAsync(user);
                mappedUser.Roles = [.. roles];
                userDtos.Add(mappedUser);
            }

            return userDtos;
        }
    }
}
