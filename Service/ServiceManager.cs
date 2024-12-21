using Contracts;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IFacultyService> _facultyService;
        private readonly Lazy<IDepartmentService> _departmentService;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger)
        {
            _facultyService = new Lazy<IFacultyService>(() => new FacultyService(repositoryManager, logger));
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, logger));
        }

        public IFacultyService FacultyService => _facultyService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
    }
}
