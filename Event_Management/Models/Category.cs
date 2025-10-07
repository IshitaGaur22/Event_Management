using System.ComponentModel.DataAnnotations;

namespace Event_Management.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        //public ICollection<Event> Events { get; set; }
    }
}
