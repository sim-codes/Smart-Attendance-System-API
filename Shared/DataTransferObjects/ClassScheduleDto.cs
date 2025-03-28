﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassScheduleDto
    {
        public Guid Id { get; set; }
        public string DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public string CourseTitle { get; set; }
        public string Classroom { get; set; }
        public string LecturerId { get; set; }
        public Guid CourseId { get; set; }
        public Guid LevelId { get; set; }
        public Guid DepartmentId { get; set; }
        public Guid ClassroomId { get; set; }
    }
}
