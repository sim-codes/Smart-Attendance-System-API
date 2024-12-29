using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AcademicSession
    {
        [Column("AcademicSessionId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Session Name is required")]
        [StringLength(60, ErrorMessage = "Session Name can't be longer than 60 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Session Start Date is required")]
        public DateOnly? StartDate { get; set; }

        [Required(ErrorMessage = "Session End Date is required")]
        public DateOnly? EndDate { get; set; }

        public bool? IsActive { get; set; }
    }
}
