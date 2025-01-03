using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Attendance
    {
        [Column("AttendanceId")]
        public Guid Id { get; set; }

        public DateTime RecordedAt { get; set; }

        public AttendanceStatus Status { get; set; }

        public string? Notes { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }

    public enum AttendanceStatus
    {
        Present,
        Absent,
        Late,
        Excused
    }
}
