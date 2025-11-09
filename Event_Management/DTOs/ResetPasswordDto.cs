using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters.")]
        [RegularExpression(
        @"^[A-Z](?=.*[a-z])(?=.*\d).*$",
        ErrorMessage = "Password must be start with an uppercase letter, and contain at least one number and one lowercase letter.")]
        public string NewPassword { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
