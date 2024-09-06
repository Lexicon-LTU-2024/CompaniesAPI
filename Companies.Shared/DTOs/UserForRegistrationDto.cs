using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Companies.Shared.DTOs
{
    public record UserForRegistrationDto : EmployeeUpdateDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; init; }

        [Required]
        public string? Role { get; init; }

        [Required]
        public string? CompanyId { get; init; }

    }
}
