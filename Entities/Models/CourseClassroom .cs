using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CourseClassroom
    {
        [Column("CourseClassroomId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Course))]
        public Guid CourseId { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Course? Course { get; set; }

        [ForeignKey(nameof(Classroom))]
        public Guid ClassroomId { get; set; }
        public Classroom? Classroom { get; set; }
    }
}
