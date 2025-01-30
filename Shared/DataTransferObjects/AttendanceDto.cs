using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AttendanceDto
    {
        public Guid Id { get; init; }
        public DateTime RecordedAt { get; init; }
        public string Status { get; init; }
        public string? Notes { get; init; }
        public Guid StudentId { get; init; }
        public Guid CourseId { get; init; }
    }
}
