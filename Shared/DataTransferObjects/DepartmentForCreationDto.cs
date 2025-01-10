using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record DepartmentForCreationDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
