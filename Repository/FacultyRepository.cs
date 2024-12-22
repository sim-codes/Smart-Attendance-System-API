using Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class FacultyRepository : RepositoryBase<Faculty>, IFacultyRepository
    {
        public FacultyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Faculty> GetAllFaculties(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();

        public Faculty GetFaculty(Guid facultyId, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(facultyId), trackChanges)
            .SingleOrDefault();

        public void CreateFaculty(Faculty faculty) => Create(faculty);
    }
}
