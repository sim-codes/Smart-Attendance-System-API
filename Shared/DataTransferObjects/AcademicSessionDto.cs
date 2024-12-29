using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record AcademicSessionDto
    {
        public Guid Id { get; init; }
        public string? Name { get; init; }
        public DateOnly StartDate { get; init; }
        public DateOnly EndDate { get; init; }
        public bool IsActive { get; init; }
    }
}
