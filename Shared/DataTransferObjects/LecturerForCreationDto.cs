using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LecturerForCreationDto
    {
        [Required(ErrorMessage = "Staff ID is a required field.")]
        public string? StaffId { get; set; }
        [Required(ErrorMessage = "Department ID is a required field.")]
        public Guid DepartmentId { get; set; }
    }
}
