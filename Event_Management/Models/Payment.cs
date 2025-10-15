using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        [ForeignKey(nameof(Booking))]
        public int BookingId { get; set; }
        [ForeignKey(nameof(Event))]
        public int EventID { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; } // e.g., Completed, Failed
        public virtual Booking Booking { get; set; }
        //public virtual Ticket Ticket { get; set; }

    }
}
