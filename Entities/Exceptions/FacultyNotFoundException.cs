using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class FacultyNotFoundException : NotFoundException
    {
        public FacultyNotFoundException(Guid facultyId)
            : base($"The Faculty with id: {facultyId} doesn't exist in the database.")
        {
        }
    }
}
