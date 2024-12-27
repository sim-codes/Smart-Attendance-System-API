using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(string userId)
            : base($"The Student with user id: {userId} doesn't exist in the database.")
        {
        }
    }
}
