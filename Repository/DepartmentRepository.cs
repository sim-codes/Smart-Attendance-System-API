using Entities.Models;
using Contracts;

namespace Repository
{
    public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Department> GetDepartments(Guid facultyId, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId), trackChanges)
            .OrderBy(e => e.Name).ToList();

        public Department GetDepartment(Guid facultyId, Guid id, bool trackChanges) =>
            FindByCondition(e => e.FacultyId.Equals(facultyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefault();

        public void CreateDepartment(Guid facultyId, Department department)
        {
            department.FacultyId = facultyId;
            Create(department);
        }
    }
} 
