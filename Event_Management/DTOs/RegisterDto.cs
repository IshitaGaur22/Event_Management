using Event_Management.Aspects;
using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@gmail.com$", ErrorMessage = "Email must be a valid Gmail address")]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        [RegularExpression(
        @"^[A-Z](?=.*[a-z])(?=.*\d).*$",
        ErrorMessage = "Password must be start with an uppercase letter, and contain at least one number and one lowercase letter.")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("Organiser|User", ErrorMessage = "Role must be 'Organiser' or 'User'")]
        public string Role { get; set; }

        [Required]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public long PhoneNumber { get; set; }

        [Required]
        public string Location { get; set; }
    }
}