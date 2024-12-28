using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record ResetPasswordDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Token is required")]
        public string? Token { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string? NewPassword { get; set; }
    }
}
