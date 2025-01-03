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
        IClassroomService ClassroomService { get; }
        IProfileService ProfileService { get; }
        IStudentService StudentService { get; }
        ILecturerService LecturerService { get; }
        IAcademicSessionService AcademicSessionService { get; }
        IClassScheduleService ClassScheduleService { get; }
        IAttendanceService AttendanceService { get; }
    }
}
