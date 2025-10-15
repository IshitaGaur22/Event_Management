using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace Event_Management.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Event))]
        public int EventId { get; set; }

        //[ForeignKey(nameof(Ticket))]
        //public int TicketId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int SelectedSeats { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }

        [Required]
        public string Status { get; set; } = "Pending"; // Default before payment

        //public virtual Ticket Ticket { get; set; }
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}
