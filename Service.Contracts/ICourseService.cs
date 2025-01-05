using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetDepartmentCourses(Guid departmentId, bool trackChanges);
        CourseDto GetDepartmentCourse(Guid departmentId, Guid id, bool trackChanges);
        CourseDto CreateCourseForDepartment(Guid departmentId, CourseForCreationDto course, bool trackChanges);
    }
}
