using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassScheduleForUpdateDto
    {
        public string DayOfWeek { get; init; }
        public TimeOnly StartTime { get; init; }
        public TimeOnly EndTime { get; init; }
        public string SessionId { get; init; }
        public Guid CourseId { get; init; }
        public Guid LevelId { get; init; }
        public Guid DepartmentId { get; init; }
        public Guid ClassroomId { get; init; }
    }
}
