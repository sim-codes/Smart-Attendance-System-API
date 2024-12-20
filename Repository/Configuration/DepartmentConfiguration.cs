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
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
                (
                    new Department
                    {
                        Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Name = "Computer Science",
                        Code = "CSC",
                        FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b")
                    },
                    new Department
                    {
                        Id = new Guid("80abbca8-664d-4b20-b5de-024705497d4a"),
                        Name = "Electrical Engineering",
                        Code = "EEE",
                        FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b")
                    }
                 );
        }
    }
}
