using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(
                new Course
                {
                    Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                    Title = "Introduction to Computer Science",
                    Code = "CSC101",
                    CreditUnits = 2,
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    LevelId = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Course
                {
                    Id = new Guid("86dba8c0-d178-41e7-938c-ed49778fb52c"),
                    Title = "Data Structures and Algorithms",
                    Code = "CSC201",
                    CreditUnits = 3,
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    LevelId = new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Course
                {
                    Id = new Guid("021ca3c1-0deb-4afd-ae94-2159a8479811"),
                    Title = "Database Management Systems",
                    Code = "CSC301",
                    CreditUnits = 3,
                    DepartmentId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                    LevelId = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870")
                }
            );
        }
    }
}