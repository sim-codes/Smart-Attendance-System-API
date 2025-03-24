using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record FaceVerificationDto
    {
        public string ImageUrl1 { get; init; }
        public string ImageUrl2 { get; init; }
    }
}
