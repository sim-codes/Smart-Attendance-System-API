using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetStudentsEnrolledForCourse(Guid courseId, bool trackChanges);
        Task<IEnumerable<string>> GetAllStudentEnrolledIdsForCourse(Guid courseId, bool trackChanges);
        IEnumerable<Enrollment> GetStudentEnrolledCourses(string userId, bool trackChanges);
        Enrollment GetCourseEnrolledByStudent(string userId, Guid id, bool trackChanges);
        void EnrollStudentForCourse(string userId, Enrollment courseEnrollment);
        void DeleteCourseEnrolledByStudent(Enrollment courseEnrollment);
    }
}
