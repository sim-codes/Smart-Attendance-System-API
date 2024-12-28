using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class LecturerNotFoundException : NotFoundException
    {
        public LecturerNotFoundException(string userId)
            : base($"The Lecturer with user id: {userId} doesn't exist in the database.")
        {
        }
    }
}
