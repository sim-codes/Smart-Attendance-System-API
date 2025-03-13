using Entities.Models;

namespace Repository.Extensions
{
    public static class RepositoryAttendanceExtensons
    {
        public static IQueryable<Attendance> Search(this IQueryable<Attendance> attendances, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return attendances;
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return attendances.Where(e => e.Course.Title.ToLower().Contains(lowerCaseSearchTerm)
            || e.User.LastName.ToLower().Contains(lowerCaseSearchTerm)
            || e.User.FirstName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Attendance> FilterByStudent(this IQueryable<Attendance> attendances, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return attendances;
            return attendances.Where(e => e.UserId.Equals(userId));
        }

        public static IQueryable<Attendance> FilterByCourse(this IQueryable<Attendance> attendances, Guid courseId)
        {
            if (courseId == Guid.Empty)
                return attendances;
            return attendances.Where(e => e.CourseId.Equals(courseId));
        }
    }
}
