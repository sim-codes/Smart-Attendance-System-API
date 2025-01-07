using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class RepositoryLecturerExtensions
    {
        public static IQueryable<Lecturer> Search(this IQueryable<Lecturer> lecturers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return lecturers;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
            return lecturers.Where(e => e.User.FirstName.ToLower().Contains(lowerCaseSearchTerm) ||
                                       e.User.LastName.ToLower().Contains(lowerCaseSearchTerm));
        }
    }
}
