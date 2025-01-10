using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassroomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
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
