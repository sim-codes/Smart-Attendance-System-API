using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Level
    {
        [Column("LevelId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Level name is a required field.")]
        [MaxLength(3, ErrorMessage = "Maximum length for the Name is 3 characters.")]
        public string? Name { get; set; }

        public ICollection<Course>? Courses { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<ClassSchedule>? ClassSchedules { get; set; }
    }
}
