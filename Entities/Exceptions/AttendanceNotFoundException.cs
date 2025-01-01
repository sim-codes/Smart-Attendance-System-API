using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class AttendanceNotFoundException : NotFoundException
    {
        public AttendanceNotFoundException(Guid attendanceId)
            : base($"The Attendance with id: {attendanceId} doesn't exist in the database.")
        {
        }
    }
}
