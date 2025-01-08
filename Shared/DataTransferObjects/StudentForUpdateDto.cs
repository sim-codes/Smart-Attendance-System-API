using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record StudentForUpdateDto
    {
        public string? MatriculationNumber { get; set; }
        public Guid LevelId { get; set; }
        public Guid DepartmentId { get; set; }

        // User details
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
