using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ClassSchedule
    {
        [Column("ClassScheduleId")]
        public Guid Id { get; set; }

        [Required]
        public string? DayOfWeek { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public TimeOnly EndTime { get; set; }

        [Required]
        [ForeignKey(nameof(AcademicSession))]
        public Guid SessionId { get; set; }
        public virtual AcademicSession? AcademicSession { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Course? Course { get; set; }

        [ForeignKey(nameof(Classroom))]
        public Guid ClassroomId { get; set; }
        [Required]
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Classroom? Classroom { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
