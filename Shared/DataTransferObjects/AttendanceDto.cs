using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AttendanceDto(Guid Id, DateTime RecordedAt, string Status, string? Notes, Guid StudentId, Guid CourseId);
}
