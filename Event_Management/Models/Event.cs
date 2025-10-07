using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Event_Management.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        [MaxLength(20)]
        public string EventName { get; set; }

        [Required]
        [MaxLength(50)]
        public string EventDescription { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeOnly Time { get; set; }

        [Required]
        [MaxLength(50)]
        public string Location { get; set; }

        //[Required]
        //public Image img { get; set; }

        //[Required]
        //[ForeignKey(nameof(Category))]
        //public int CategoryID { get; set; }

        public Category Category { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
