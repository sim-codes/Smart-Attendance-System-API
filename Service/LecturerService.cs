using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class LecturerService : ILecturerService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private UserManager<User> _userManager;

        public LecturerService(IRepositoryManager repository,
            ILoggerManager logger, IMapper mapper,
            UserManager<User> userManager
            )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<LecturerDto> CreateLecturer(LecturerForCreationDto lecturer)
        {
            var user = await _userManager.FindByIdAsync(lecturer.UserId);
            if (user == null)
                throw new UserNotFoundException(lecturer.UserId);

            var lecturerEntity = _mapper.Map<Lecturer>(lecturer);
            lecturerEntity.User = user;

            _repository.Lecturer.CreateLecturer(lecturerEntity);
            await _repository.SaveAsync();

            var createdLecturer = _mapper.Map<LecturerDto>(lecturerEntity);
            return createdLecturer;
        }

        public async Task<LecturerDto> GetLecturerAsync(string userId, bool trackChanges)
        {
            var lecturer = _repository.Lecturer.GetLecturerAsync(userId, trackChanges);
            if (lecturer is null)
                throw new LecturerNotFoundException(userId);

            var lecturerDto = _mapper.Map<LecturerDto>(lecturer);
            return lecturerDto;
        }

        public async Task<IEnumerable<LecturerDto>> GetLecturersAsync(bool trackChanges)
        {
            var lecturers = await _repository.Lecturer.GetAllLecturersAsync(trackChanges);
            var lecturersDto = _mapper.Map<IEnumerable<LecturerDto>>(lecturers);
            return lecturersDto;
        }
    }
}
