using Entities.Models;

namespace Contracts
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetDepartmentalCourses(Guid departmentId, bool trackChanges);
        Course GetDepartmentalCourse(Guid departmentId, Guid id, bool trackChanges);
        void CreateDepartmentalCourse(Guid departmentId, Course course);
    }
}
