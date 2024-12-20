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
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.HasData
                (
                    new Faculty
                    {
                        Id = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-1b1b1b1b1b1b"),
                        Name = "Faculty of Engineering",
                        Code = "FOE"
                    },
                    new Faculty
                    {
                        Id = new Guid("b4e2e3f2-3b3d-4b5d-8b0b-2b2b2b2b2b2b"),
                        Name = "Faculty of Science",
                        Code = "FOS"
                    }
                 );
        }
    }
}
