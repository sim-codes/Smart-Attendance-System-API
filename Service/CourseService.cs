using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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
    internal sealed class CourseService : ICourseService
    {
        private ILoggerManager _logger;
        private IRepositoryManager _repository;
        private IMapper _mapper;

        public CourseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public IEnumerable<CourseDto> GetDepartmentCourses(Guid departmentId, CourseParameters courseParameters, bool trackChanges)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var coursesFromDb = _repository.Course.GetDepartmentalCourses(departmentId, courseParameters, trackChanges);
            var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(coursesFromDb);
            return coursesDto;
        }

        public CourseDto GetDepartmentCourse(Guid departmentId, Guid id, bool trackChanges)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);
            var courseFromDb = _repository.Course.GetDepartmentalCourse(departmentId, id, trackChanges);
            if (courseFromDb is null)
                throw new CourseNotFoundException(id);
            var courseDto = _mapper.Map<CourseDto>(courseFromDb);
            return courseDto;
        }

        public CourseDto CreateCourseForDepartment(Guid departmentId, CourseForCreationDto course, bool trackChanges)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var courseEntity = _mapper.Map<Course>(course);

            _repository.Course.CreateDepartmentalCourse(departmentId, courseEntity);
            _repository.Save();

            var courseDto = _mapper.Map<CourseDto>(courseEntity);
            return courseDto;
        }

        public void DeleteCourseForDepartment(Guid departmentId, Guid id)
        {
            var department = _repository.Department.GetDepartment(departmentId, trackChanges: false);
            if (department is null)
                throw new DepartmentNotFoundException(departmentId);

            var course = _repository.Course.GetDepartmentalCourse(departmentId, id, trackChanges: false);
            if (course is null)
                throw new CourseNotFoundException(id);

            _repository.Course.DeleteDepartmentalCourse(course);
            _repository.Save();
        }
    }
}
