using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IEnrollmentService
    {
        IEnumerable<EnrollmentDto> GetAllCoursesEnrolledByStudent(string userId, bool trackChanges);
        EnrollmentDto GetCourseEnrolledByStudent(string userId, Guid courseId, bool trackChanges);
        EnrollmentDto EnrollStudentForCourse(string userId, EnrollmentForCreationDto enrollmentForCreation, bool trackChanges);
        void DeleteCourseEnrolledByStudent(string userId, Guid courseId, bool trackChanges);
        Task<IEnumerable<StudentDto>> GetStudentsEnrolledByCourse(Guid CourseId, bool trackChanges);
    }
}
