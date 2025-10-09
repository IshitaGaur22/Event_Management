using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        [Required]
        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }
        [Required]
        public int TotalSeats { get; set; }

        [Required]
        [Precision(18, 2)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerTicket { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
} 