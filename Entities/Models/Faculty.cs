using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Faculty
    {
        [Column("FacultyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Faculty name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Faculty code is a required field.")]
        [MaxLength(10, ErrorMessage = "Maximum length for the Code is 10 characters.")]
        public string? Code { get; set; }

        public ICollection<Department>? Departments { get; set; }
    }
}
