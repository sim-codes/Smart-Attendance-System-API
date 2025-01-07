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
    public class LecturerRepository : RepositoryBase<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<Lecturer>> GetAllLecturersAsync(LecturerParameters lecturerParameters, bool trackChanges)
        {
            var lecturers = await FindAll(trackChanges)
                .Include(l => l.User)
                .Include(l => l.Department)
                .Search(lecturerParameters.SearchTerm)
                .ToListAsync();

            return PagedList<Lecturer>
                .ToPagedList(lecturers, lecturerParameters.PageNumber, lecturerParameters.PageSize);
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
