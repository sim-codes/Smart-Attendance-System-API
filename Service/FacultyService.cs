using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class FacultyService : IFacultyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public FacultyService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<FacultyDto> GetAllFaculties(bool trackChanges)
        {
            var faculties = _repository.Faculty.GetAllFaculties(trackChanges);

             var facultiesDto = _mapper.Map<IEnumerable<FacultyDto>>(faculties);

            return facultiesDto;
        }

        public FacultyDto GetFaculty(Guid id, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(id, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(id);

            var facultyDto = _mapper.Map<FacultyDto>(faculty);
            return facultyDto;
        }

        public FacultyDto CreateFaculty(FacultyForCreationDto faculty)
        {
            var facultyEntity = _mapper.Map<Faculty>(faculty);
            _repository.Faculty.CreateFaculty(facultyEntity);
            _repository.Save();

            var facultyToReturn = _mapper.Map<FacultyDto>(facultyEntity);
            return facultyToReturn;
        }
    }
}
