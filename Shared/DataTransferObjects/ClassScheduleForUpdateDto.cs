using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassScheduleForUpdateDto(string DayOfWeek, TimeOnly StartTime, TimeOnly EndTime, string SessionId, Guid CourseId, Guid ClassroomId);
}
