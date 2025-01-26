using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Repository.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasData
            (
                new Student
                {
                    Id = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    MatriculationNumber = "21/0611",
                    UserId = "fca7f097-444b-43ea-9bb4-c94243d09021",
                    LevelId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), // Assuming this is Level 100
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") // Assuming this is a valid DepartmentId
                }
            );
        }
    }
}
