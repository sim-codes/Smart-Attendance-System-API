using System.ComponentModel.DataAnnotations;

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

        [Required]
        public double StudentLon { get; init; }

        [Required]
        public double StudentLat { get; init; }
    }
}
