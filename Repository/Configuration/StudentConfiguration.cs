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
                    UserId = "b65a0ac9-72bf-4e24-8406-4de1339199d2",
                    LevelId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"), // Assuming this is Level 100
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") // Assuming this is a valid DepartmentId
                }
            );
        }
    }
}
