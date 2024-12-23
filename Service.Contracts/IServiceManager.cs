using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        IFacultyService FacultyService { get; }
        IDepartmentService DepartmentService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}
