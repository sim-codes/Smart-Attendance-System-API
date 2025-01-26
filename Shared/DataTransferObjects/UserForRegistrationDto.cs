using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record UserForRegistrationDto
    {
        [Required]
        public string FirstName { get; init; }
        public string LastName { get; init; }
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; init; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; init; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }
        public string PhoneNumber { get; init; }
        public string ProfileImageUrl { get; init; }
        public ICollection<string> Roles { get; init; }
    }
}
