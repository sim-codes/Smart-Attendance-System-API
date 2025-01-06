using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts
{
    public interface ICourseRepository
    {
        PagedList<Course> GetDepartmentalCourses(Guid departmentId, CourseParameters courseParameters, bool trackChanges);
        Course GetDepartmentalCourse(Guid departmentId, Guid id, bool trackChanges);
        void CreateDepartmentalCourse(Guid departmentId, Course course);
        void DeleteDepartmentalCourse(Course course);
    }
}
