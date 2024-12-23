using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2d",
                    Name = "Student",
                    NormalizedName = "STUDENT"
                },
                new IdentityRole
                {
                    Id = "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2e",
                    Name = "Lecturer",
                    NormalizedName = "LECTURER"
                },
                new IdentityRole
                {
                    Id = "8f7d6e5c-4b3a-2f1d-9e8d-7c6b5a4f3e2f",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
            );
        }
    }
}
