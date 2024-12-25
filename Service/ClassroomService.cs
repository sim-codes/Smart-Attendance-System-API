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
    internal sealed class ClassroomService : IClassroomService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ClassroomService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public ClassroomDto CreateClassroom(Guid facultyId, ClassroomForCreationDto classroom, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public ClassroomDto GetClassroom(Guid facultyId, Guid id, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(facultyId, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(facultyId);

            var classroomFromDb = _repository.Classroom.GetClassroom(facultyId, id, trackChanges);
            if (classroomFromDb is null)
                throw new DepartmentNotFoundException(id);

            var classroomDto = _mapper.Map<ClassroomDto>(classroomFromDb);
            return classroomDto;
        }

        public IEnumerable<ClassroomDto> GetClassrooms(Guid facultyId, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(facultyId, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(facultyId);

            var classroomFromDb = _repository.Classroom.GetClassrooms(facultyId, trackChanges);
            var classroomsDto = _mapper.Map<IEnumerable<ClassroomDto>>(classroomFromDb);
            return classroomsDto;
        }
    }
}
