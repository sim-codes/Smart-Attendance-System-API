using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AttendanceForCreationDto
    {
        [Required]
        public string Status { get; init; }
        public string? Notes { get; init; }

        [Required]
        public Guid StudentId { get; init; }

        [Required]
        public Guid CourseId { get; init; }
    }
}
