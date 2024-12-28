using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StudentRepository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges)
        {
            var students = await FindAll(trackChanges)
            .Include(s => s.User)
            .Include(s => s.Level)
            .Include(s => s.Department)
            .ToListAsync();
            return students;
        }

        public async Task<Student> GetStudentAsync(string userId, bool trackChanges)
        {
            var student = await FindByCondition(s => s.UserId.Equals(userId), trackChanges)
                .Include(s => s.User)
                .Include(s => s.Level)
                .Include(s => s.Department)
                .SingleOrDefaultAsync();
            return student;
        }

        public void CreateStudent(string userId, Student student)
        {
            student.UserId = userId;
            Create(student);
        }
    }
}
