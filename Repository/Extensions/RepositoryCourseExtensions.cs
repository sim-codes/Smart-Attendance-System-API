using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryCourseExtensions
    {
        public static IQueryable<Course> Search(this IQueryable<Course> courses, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return courses;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return courses.Where(e => e.Title.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
