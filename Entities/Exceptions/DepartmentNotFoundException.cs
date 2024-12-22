using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class DepartmentNotFoundException : NotFoundException
    {
        public DepartmentNotFoundException(Guid departmentId)
            : base($"The Department with id: {departmentId} doesn't exist in the database.")
        {
        }
    }
}
