using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    public class Enrollment
    {
        [Column("EnrollmentId")]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public virtual User? User { get; set; }

        [Required]
        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Course? Course { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
