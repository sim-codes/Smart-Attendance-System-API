using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department
    {
        [Column("DepartmentId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Department name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Department code is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Code is 10 characters.")]
        public string? Code { get; set; }

        [ForeignKey(nameof(Faculty))]
        public Guid FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
    }
}
