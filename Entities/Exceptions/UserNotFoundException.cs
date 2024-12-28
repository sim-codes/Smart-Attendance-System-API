using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userIdOrEmail)
            : base($"The User with {userIdOrEmail} doesn't exist in the database.")
        { }
    }
}
