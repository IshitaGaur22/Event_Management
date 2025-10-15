namespace Event_Management.DTOs
{
    public class BookingHistoryDto
    {
        public int BookingId { get; set; }
        public string EventName { get; set; }
        public DateOnly EventDate { get; set; }
        public string Location { get; set; }
        public int SelectedSeats { get; set; }
        public decimal PricePerTicket { get; set; }
        public DateOnly BookingDate { get; set; }
        public string Status { get; set; }
    }
}