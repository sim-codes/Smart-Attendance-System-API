using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Repository.Configuration
{
    public class ClassroomConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> builder)
        {
            builder.HasData
            (
                new Classroom
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Engineering Lab 1",
                    TopLeftLat = 6.8920,
                    TopLeftLon = 3.7230,
                    TopRightLat = 6.8920,
                    TopRightLon = 3.7235,
                    BottomLeftLat = 6.8915,
                    BottomLeftLon = 3.7230,
                    BottomRightLat = 6.8915,
                    BottomRightLon = 3.7235,
                    FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b")  // Engineering Faculty
                },
                new Classroom
                {
                    Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce4"),
                    Name = "Engineering Lab 2",
                    TopLeftLat = 6.8925,
                    TopLeftLon = 3.7240,
                    TopRightLat = 6.8925,
                    TopRightLon = 3.7245,
                    BottomLeftLat = 6.8920,
                    BottomLeftLon = 3.7240,
                    BottomRightLat = 6.8920,
                    BottomRightLon = 3.7245,
                    FacultyId = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b")  // Engineering Faculty
                }
            );
        }
    }
}