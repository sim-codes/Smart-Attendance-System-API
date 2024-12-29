using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AcademicSessionForCreationDto
    {
        [Required(ErrorMessage = "Session Name is required")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Session Start Date is required")]
        public DateOnly StartDate { get; init; }

        [Required(ErrorMessage = "Session End Date is required")]
        public DateOnly EndDate { get; init; }

        [Required(ErrorMessage = "Session Status is required")]
        public bool IsActive { get; init; }
    }
}
