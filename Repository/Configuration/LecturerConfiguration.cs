using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
    {
        public void Configure(EntityTypeBuilder<Lecturer> builder)
        {
            builder.HasData
            (
                new Lecturer
                {
                    Id = new Guid("8f47b996-12c3-4178-a69f-412b56b8f155"),
                    StaffId = "AU22/07623",
                    UserId = "0c2b7446-f77d-4c39-aa91-963ec72d627a",
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") // Assuming this is a valid DepartmentId
                }
            );
        }
    }
}
