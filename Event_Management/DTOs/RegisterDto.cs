using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }
        public long PhoneNumber { get; set; }
        public string Location { get; set; }
    }
}

