namespace Event_Management.DTOs
{
    public class BookingHistoryDto
    {
        public int BookingId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int SelectedSeats { get; set; }
        public decimal PricePerTicket { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
    }
}