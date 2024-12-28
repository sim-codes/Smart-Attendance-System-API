using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record LecturerDto
    {
        public string? UserId { get; set; }
        public string? StaffId { get; set; }
        public string? Department { get; set; }

        // User details
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
