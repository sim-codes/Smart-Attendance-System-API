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
    public class Lecturer
    {
        [Column("LecturerId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Staff ID is a required field.")]
        public string? StaffId { get; set; }

        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Department))]
        public Guid DepartmentId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Department? Department { get; set; }
    }
}
