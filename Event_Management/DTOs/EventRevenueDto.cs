using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class EventRevenueDto
    {
        [Key]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateOnly EventDate { get; set; }
        public string Location { get; set; }
        public int TotalSeats { get; set; }
        public decimal PricePerTicket { get; set; }
        public decimal EstimatedRevenue { get; set; }
        public decimal ActualRevenue { get; set; }
    }

}
