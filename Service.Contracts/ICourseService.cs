using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetDepartmentCourses(Guid departmentId, CourseParameters courseParameters,bool trackChanges);
        CourseDto GetDepartmentCourse(Guid departmentId, Guid id, bool trackChanges);
        CourseDto CreateCourseForDepartment(Guid departmentId, CourseForCreationDto course, bool trackChanges);
        void DeleteCourseForDepartment(Guid departmentId, Guid id);
    }
}
