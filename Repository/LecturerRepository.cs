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
    public class LecturerRepository : RepositoryBase<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Lecturer>> GetAllLecturersAsync(bool trackChanges)
        {
            var lecturers = await FindAll(trackChanges)
                .Include(l => l.User)
                .Include(l => l.Department)
                .ToListAsync();
            return lecturers;
        }

        public async Task<Lecturer> GetLecturerAsync(string userId, bool trackChanges)
        {
            var lecturer = await FindByCondition(l => l.UserId.Equals(userId), trackChanges)
                .Include(l => l.User)
                .Include(l => l.Department)
                .SingleOrDefaultAsync();
            return lecturer;
        }

        public void CreateLecturer(string userId, Lecturer lecturer)
        {
            lecturer.UserId = userId;
            Create(lecturer);
        }
    }
}
