using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        
        [Required]
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        [Required]
        public int TotalSeats { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }

        public virtual Event Event { get; set; }

        //[Required]
        //public int EventID { get; set; }

        //public Event Event { get; set; }
    }
} 