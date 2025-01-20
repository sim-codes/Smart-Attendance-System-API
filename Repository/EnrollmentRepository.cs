using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EnrollmentRepository : RepositoryBase<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(RepositoryContext context)
            : base(context)
        {
        }

        public void DeleteCourseEnrolledByStudent(Enrollment courseEnrollment) => Delete(courseEnrollment);

        public void EnrollStudentForCourse(string userId, Enrollment courseEnrollment) => Create(courseEnrollment);

        public Enrollment GetCourseEnrolledByStudent(string userId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public IEnumerable<Enrollment> GetStudentEnrolledCourses(string userId, bool trackChanges) =>
            FindByCondition(e => e.UserId.Equals(userId), trackChanges)
            .OrderBy(e => e.EnrollmentDate).ToList();
    }
}
