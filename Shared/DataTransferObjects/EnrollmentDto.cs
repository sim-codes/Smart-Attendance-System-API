using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EnrollmentDto
    {
        public Guid Id { get; init; }
        public Guid CourseId { get; init; }
        public string CourseTitle { get; init; }
        public string CourseCode { get; init; }
        public int CreditUnits { get; init; }
        public DateTime EnrollmentDate { get; init; }

    }
}
