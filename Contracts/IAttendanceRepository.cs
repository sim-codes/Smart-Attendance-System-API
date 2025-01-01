using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAttendanceRepository
    {
        IEnumerable<Attendance> GetAllAttendances(bool trackChanges);
        Attendance GetAttendanceById(Guid attendanceId, bool trackChanges);
        void CreateAttendance(Attendance attendance);
    }
}
