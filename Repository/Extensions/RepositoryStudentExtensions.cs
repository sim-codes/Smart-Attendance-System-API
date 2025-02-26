using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryStudentExtensions
    {
        public static IQueryable<Student> Search(this IQueryable<Student> students, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return students;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return students.Where(e => e.User.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                                       e.User.LastName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Student> FilterStudentsByDepartment(this IQueryable<Student> students, Guid departmentId)
        {
            if (departmentId == Guid.Empty)
                return students;
            return students.Where(e => e.DepartmentId.Equals(departmentId));
        }
            
    }
}
