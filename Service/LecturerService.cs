using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
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

        public async Task<LecturerDto> CreateLecturer(string userId, LecturerForCreationDto lecturer)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                throw new UserNotFoundException(userId);

            var lecturerEntity = _mapper.Map<Lecturer>(lecturer);
            lecturerEntity.User = user;

            _repository.Lecturer.CreateLecturer(userId, lecturerEntity);
            await _repository.SaveAsync();

            var createdLecturer = _mapper.Map<LecturerDto>(lecturerEntity);
            return createdLecturer;
        }

        public async Task<LecturerDto> GetLecturerAsync(string userId, bool trackChanges)
        {
            var lecturer = await _repository.Lecturer.GetLecturerAsync(userId, trackChanges);
            if (lecturer is null)
                throw new LecturerNotFoundException(userId);

            var lecturerDto = _mapper.Map<LecturerDto>(lecturer);
            return lecturerDto;
        }

        public async Task<(IEnumerable<LecturerDto> lecturers, MetaData metaData)> GetLecturersAsync(LecturerParameters lecturerParameters, bool trackChanges)
        {
            var lecturersWithMetaData = await _repository.Lecturer.GetAllLecturersAsync(lecturerParameters, trackChanges);
            var lecturersDto = _mapper.Map<IEnumerable<LecturerDto>>(lecturersWithMetaData);
            return (lecturers: lecturersDto, metaData: lecturersWithMetaData.MetaData);
        }

        public async Task UpdateLecturer(string userId, LecturerForUpdateDto lecturer)
        {
            var lecturerEntity = await _repository.Lecturer.GetLecturerAsync(userId, trackChanges: true);

            if (lecturerEntity is null)
                throw new LecturerNotFoundException(userId);

            _mapper.Map(lecturer, lecturerEntity);
            await _repository.SaveAsync();
        }
    }
}
