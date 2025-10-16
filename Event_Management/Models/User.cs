using System.ComponentModel.DataAnnotations;

namespace Event_Management.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        public string Email { get; set; }    
    }
}
