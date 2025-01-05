using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments(Guid facultyId, bool trackChanges);
        Department GetFacultyDepartment(Guid facultyId, Guid id, bool trackChanges);
        Department GetDepartment(Guid id, bool trackChanges);
        void CreateDepartment(Guid facultyId, Department department);
    }
}
