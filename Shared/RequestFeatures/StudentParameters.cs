using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class StudentParameters : RequestParameters
    {
        public string? SearchTerm { get; set; }
    }
}
