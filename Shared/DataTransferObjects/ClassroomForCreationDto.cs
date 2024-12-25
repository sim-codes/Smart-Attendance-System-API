using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassroomForCreationDto
    {
        [Required(ErrorMessage = "Classroom name is required")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Location coordinates are required")]
        public double TopLeftLat { get; set; }
        public double TopLeftLon { get; set; }
        public double TopRightLat { get; set; }
        public double TopRightLon { get; set; }
        public double BottomLeftLat { get; set; }
        public double BottomLeftLon { get; set; }
        public double BottomRightLat { get; set; }
        public double BottomRightLon { get; set; }
    }
}
