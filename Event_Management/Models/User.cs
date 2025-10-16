using System.ComponentModel.DataAnnotations;

namespace Event_Management.Models
{
    public class User
    {
        

        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@gmail.com$", ErrorMessage = "Email must be a valid Gmail address ending with @gmail.com")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [RegularExpression("Organiser|User", ErrorMessage = "Role must be either 'Organiser' or 'User'")]
        public string Role { get; set; }

        [Required]
        [Phone]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public long PhoneNumber { get; set; }

        [Required]
        public string Location { get; set; }

        public string Email { get; set; }    
    }
}
