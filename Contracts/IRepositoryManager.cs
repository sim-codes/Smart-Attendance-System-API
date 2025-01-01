using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IFacultyRepository Faculty { get; }
        IDepartmentRepository Department { get; }
        IClassroomRepository Classroom { get; }
        IStudentRepository Student { get; }
        ILecturerRepository Lecturer { get; }
        IAcademicSessionRepository AcademicSession { get; }
        IClassScheduleRepository ClassSchedule { get; }
        IAttendanceRepository Attendance { get; }
        void Save();
        Task SaveAsync();
    }
}
