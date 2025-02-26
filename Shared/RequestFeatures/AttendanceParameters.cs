using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class AttendanceParameters :RequestParameters
    {
        public string? SearchTerm { get; set; }
        public string? UserId { get; set; }
        public Guid CourseId { get; set; }
    }
}
