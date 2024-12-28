using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly Lazy<IAuthenticationService> _authenticationService;
        private readonly Lazy<IClassroomService> _classroomService;
        private readonly Lazy<IProfileService> _profileService;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<ILecturerService> _lecturerService;

        public ServiceManager(IRepositoryManager repositoryManager, 
            ILoggerManager logger,
            IMapper mapper,
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _facultyService = new Lazy<IFacultyService>(() => new FacultyService(repositoryManager, logger, mapper));
            _departmentService = new Lazy<IDepartmentService>(() => new DepartmentService(repositoryManager, logger, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger, mapper, userManager, configuration));
            _classroomService = new Lazy<IClassroomService>(() => new ClassroomService(repositoryManager, logger, mapper));
            _profileService = new Lazy<IProfileService>(() => new ProfileService(logger, mapper, userManager, configuration));
            _studentService = new Lazy<IStudentService>(() => new StudentService(repositoryManager, logger, mapper, userManager));
            _lecturerService = new Lazy<ILecturerService>(() => new LecturerService(repositoryManager, logger, mapper, userManager));
        }

        public IFacultyService FacultyService => _facultyService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
        public IClassroomService ClassroomService => _classroomService.Value;
        public IProfileService ProfileService => _profileService.Value;
        public IStudentService StudentService => _studentService.Value;
        public ILecturerService LecturerService => _lecturerService.Value;
    }
}
