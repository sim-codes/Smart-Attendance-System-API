using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
