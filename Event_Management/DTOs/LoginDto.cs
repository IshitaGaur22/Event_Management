using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}