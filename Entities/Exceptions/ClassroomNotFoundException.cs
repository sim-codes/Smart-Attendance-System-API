using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class ClassroomNotFoundException : NotFoundException
    {
        public ClassroomNotFoundException(Guid classroomId)
            : base($"The Classroom with id: {classroomId} doesn't exist in the database.")
        {
        }
    }
}
