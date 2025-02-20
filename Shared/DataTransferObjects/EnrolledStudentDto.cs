using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public class EnrolledStudentDto
    {
        public Guid Id { get; init; }
        public Guid CourseId { get; init; }
        public string StudentId { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Email { get; init; }

    }
}