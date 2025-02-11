using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public UserProfileDto User { get; set; }
        public string ErrorMessage { get; set; }
    }
}
