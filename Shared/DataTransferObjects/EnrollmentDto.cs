﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record EnrollmentDto(Guid Id, DateTime EnrollmentDate, string UserId, Guid CourseId);
}