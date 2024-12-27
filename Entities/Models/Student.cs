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
    public class Student
    {
        [Column("StudentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Matriculation number is a required field.")]
        public string? MatriculationNumber { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Level))]
        public Guid LevelId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Level? Level { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Department? Department { get; set; }
    }
}
