using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAttendanceService
    {
        IEnumerable<AttendanceDto> GetAttendances(bool trackChanges);
        AttendanceDto GetAttendance(Guid attendanceId, bool trackChanges);
        AttendanceDto CreateAttendance(AttendanceForCreationDto attendance);
    }
}
