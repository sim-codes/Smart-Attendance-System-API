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
    internal sealed class DepartmentService : IDepartmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public DepartmentService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<DepartmentDto> GetDepartments(Guid facultyId, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(facultyId, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(facultyId);

            var departmentsFromDb = _repository.Department.GetDepartments(facultyId, trackChanges);
            var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departmentsFromDb);
            return departmentsDto;
        }

        public DepartmentDto GetDepartment(Guid facultyId, Guid id, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(facultyId, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(facultyId);

            var departmentFromDb = _repository.Department.GetFacultyDepartment(facultyId, id, trackChanges);
            if (departmentFromDb is null)
                throw new DepartmentNotFoundException(id);

            var departmentDto = _mapper.Map<DepartmentDto>(departmentFromDb);
            return departmentDto;
        }

        public DepartmentDto CreateDepartment(Guid facultyId, DepartmentForCreationDto department, bool trackChanges)
        {
            var faculty = _repository.Faculty.GetFaculty(facultyId, trackChanges);
            if (faculty is null)
                throw new FacultyNotFoundException(facultyId);

            var departmentEntity = _mapper.Map<Department>(department);

            _repository.Department.CreateDepartment(facultyId, departmentEntity);
            _repository.Save();

            var departmentToReturn = _mapper.Map<DepartmentDto>(departmentEntity);

            return departmentToReturn;
        }
    }
}
