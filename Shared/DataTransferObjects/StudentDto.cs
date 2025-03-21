﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record StudentDto
    {
        public string? UserId { get; set; }
        public string? MatriculationNumber { get; set; }
        public string? Level { get; set; }
        public string? Department { get; set; }

        // User details
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
