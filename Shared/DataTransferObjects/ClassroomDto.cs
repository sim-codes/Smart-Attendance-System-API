using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ClassroomDto(
        Guid Id,
        string Name,
        double TopLeftLat,
        double TopLeftLon,
        double TopRightLat,
        double TopRightLon,
        double BottomLeftLat,
        double BottomLeftLon,
        double BottomRightLat,
        double BottomRightLon
        );
}
