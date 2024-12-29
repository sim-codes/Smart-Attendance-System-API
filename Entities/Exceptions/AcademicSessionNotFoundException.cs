using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class AcademicSessionNotFoundException : NotFoundException
    {
        public AcademicSessionNotFoundException(Guid academicSessionId)
            : base($"Academic session with id {academicSessionId} not found.")
        {
        }
    }
}
