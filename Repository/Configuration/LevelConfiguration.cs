using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Repository.Configuration
{
    public class LevelConfiguration : IEntityTypeConfiguration<Level>
    {
        public void Configure(EntityTypeBuilder<Level> builder)
        {
            builder.HasData
            (
                new Level
                {
                    Id = new Guid("a1d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "100"
                },
                new Level
                {
                    Id = new Guid("b2d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "200"
                },
                new Level
                {
                    Id = new Guid("c3d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "300"
                },
                new Level
                {
                    Id = new Guid("d4d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "400"
                }
            );
        }
    }
}
