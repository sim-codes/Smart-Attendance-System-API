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
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Course? Course { get; set; }

        [Required]
        [ForeignKey(nameof(Level))]
        public Guid LevelId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Level Level { get; set; }

        [Required]
        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Department Department { get; set; }

        [Required]
        [ForeignKey(nameof(Classroom))]
        public Guid ClassroomId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Classroom Classroom { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
