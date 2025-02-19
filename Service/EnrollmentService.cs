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
    internal class EnrollmentService : IEnrollmentService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EnrollmentService(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public void DeleteCourseEnrolledByStudent(string userId, Guid courseId, bool trackChanges)
        {
            var enrollment = _repository.CourseEnrollment.GetCourseEnrolledByStudent(userId, courseId, trackChanges);
            if (enrollment is null)
                throw new EnrollmentNotFoundException(userId, courseId);
            _repository.CourseEnrollment.DeleteCourseEnrolledByStudent(enrollment);
            _repository.Save();
        }

        public EnrollmentDto EnrollStudentForCourse(string userId, EnrollmentForCreationDto enrollmentForCreation, bool trackChanges)
        {
            var enrollmentEntity = _mapper.Map<Enrollment>(enrollmentForCreation);
            _repository.CourseEnrollment.EnrollStudentForCourse(userId, enrollmentEntity);
            _repository.Save();

            var enrollmentToReturn = _mapper.Map<EnrollmentDto>(enrollmentEntity);

            return enrollmentToReturn;
        }

        public IEnumerable<EnrollmentDto> GetAllCoursesEnrolledByStudent(string userId, bool trackChanges)
        {
            var enrollments = _repository.CourseEnrollment.GetStudentEnrolledCourses(userId, trackChanges);
            var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
            return enrollmentsDto;
        }

        public EnrollmentDto GetCourseEnrolledByStudent(string userId, Guid courseId, bool trackChanges)
        {
            var enrollment = _repository.CourseEnrollment.GetCourseEnrolledByStudent(userId, courseId, trackChanges);

            if (enrollment is null)
                throw new EnrollmentNotFoundException(userId, courseId);

            var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollment);
            return enrollmentDto;
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsEnrolledByCourse(Guid CourseId, bool trackChanges)
        {
            var students = await _repository.CourseEnrollment.GetStudentsEnrolledForCourse(CourseId, trackChanges);
            var studentsDto = _mapper.Map<IEnumerable<StudentDto>>(students);
            return studentsDto;
        }
    }
}
