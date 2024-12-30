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
    public class Course
    {
        [Column("CourseId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Course Title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Course code is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Code is 10 characters.")]
        public string? Code { get; set; }

        [Required(ErrorMessage = "Course credit unit is a required field.")]
        public int CreditUnits { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }
        public virtual Department? Department { get; set; }

        [ForeignKey(nameof(Level))]
        public Guid LevelId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public virtual Level? Level { get; set; }

        public virtual ICollection<ClassSchedule>? ClassSchedules { get; set; }
    }
}
