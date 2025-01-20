using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EnrollmentNotFoundException : NotFoundException
    {
        public EnrollmentNotFoundException(string userId, Guid courseId)
            : base($"Enrollment for student with id {userId} and course with id {courseId} not found.")
        {
        }
    }
}
