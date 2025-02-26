using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;
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
        public async Task<PagedList<Student>> GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges)
        {
            var students = await FindAll(trackChanges)
                .Include(s => s.User)
                .Include(s => s.Level)
                .Include(s => s.Department)
                .Search(studentParameters.SearchTerm)
                .FilterStudentsByDepartment(studentParameters.DepartmentId)
                .ToListAsync();

            return PagedList<Student>
                .ToPagedList(students, studentParameters.PageNumber, 
                studentParameters.PageSize);
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
