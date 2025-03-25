using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IDepartmentService
    {
        IEnumerable<DepartmentDto> GetAllDepartments(bool trackChanges);
        IEnumerable<DepartmentDto> GetDepartmentsForFaculty(Guid facultyId, bool trackChanges);
        DepartmentDto GetDepartment(Guid facultyId, Guid id, bool trackChanges);
        DepartmentDto CreateDepartment(Guid facultyId, DepartmentForCreationDto department, bool trackChanges);
    }
}
