
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Classroom
    {
        [Column("ClassromId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Classroom name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Top-left coordinates are required")]
        public double TopLeftLat { get; set; }
        public double TopLeftLon { get; set; }

        [Required(ErrorMessage = "Top-right coordinates are required")]
        public double TopRightLat { get; set; }
        public double TopRightLon { get; set; }

        [Required(ErrorMessage = "Bottom-left coordinates are required")]
        public double BottomLeftLat { get; set; }
        public double BottomLeftLon { get; set; }

        [Required(ErrorMessage = "Bottom-right coordinates are required")]
        public double BottomRightLat { get; set; }
        public double BottomRightLon { get; set; }

        [ForeignKey(nameof(Faculty))]
        public Guid FacultyId { get; set; }
        public Faculty? Faculty { get; set; }

        public ICollection<ClassSchedule>? ClassSchedules { get; set; }
    }
}
