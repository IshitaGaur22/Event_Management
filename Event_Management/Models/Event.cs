using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace Event_Management.Models
{
   
        public class Event
        {
        
            [Key]
            public int EventID { get; set; }

            [StringLength(100,MinimumLength =3,ErrorMessage = "Event name must be at least 3 characters.")]
            
            public string EventName { get; set; }

            public string Description { get; set; }

            [Required(ErrorMessage = "Please enter a valid date.")]
            public DateOnly EventDate { get; set; }

            [Required(ErrorMessage = "Please enter a valid time.")]
            public TimeOnly EventTime { get; set; }

            [Required(ErrorMessage = "Location is required.")]
            public string Location { get; set; }

            //[Required(ErrorMessage ="Please Choose a category.")]
            //public int CategoryID { get; set; }
            public Category Category { get; set; }
            public ICollection<Ticket> Tickets { get; set; }



    }

    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dt)
            {
                return dt > DateTime.Now;
            }
            return false;
        }
    }
}
