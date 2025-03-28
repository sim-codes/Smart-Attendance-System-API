﻿using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string? ProfileImageUrl { get; set; }

        public virtual Student? Student { get; set; }
        public virtual Lecturer? Lecturer { get; set; }
        public virtual ICollection<Attendance>? Attendances { get; set; }
        public virtual ICollection<Enrollment>? Enrollments { get; set; }
    }
}
