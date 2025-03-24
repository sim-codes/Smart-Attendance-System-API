using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record FaceDetectionDto
    {
        public string ImageUrl { get; init; }
    }
}
