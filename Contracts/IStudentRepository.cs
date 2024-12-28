using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
        Task<Student> GetStudentAsync(string userId, bool trackChanges);
        void CreateStudent(string userId, Student student);
    }
}
