using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record CourseForCreationDto
    {
        public string Title { get; init; }
        public string Code { get; init; }
        public string Description { get; init; }
        public int CreditUnits { get; init; }
        public Guid LevelId { get; init; }
    }
}
