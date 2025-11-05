using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

       // public ICollection<Event> Events { get; set; }

    }
}


