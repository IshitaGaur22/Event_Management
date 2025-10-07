using System.ComponentModel.DataAnnotations;

namespace Event_Management.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        //public ICollection<Event> Events { get; set; }
    }
}