using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassScheduleForCreationDto
    {
        public string? DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public Guid SessionId { get; set; }
        public Guid CourseId { get; set; }
        public Guid LevelId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid ClassroomId { get; set; }
    }
}
