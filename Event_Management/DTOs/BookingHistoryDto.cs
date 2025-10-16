namespace Event_Management.DTOs
{
    public class BookingHistoryDTO
    {
        public int BookingId { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly EventTime { get; set; }
        public int SelectedSeats { get; set; }
        public string Status { get; set; }
    }
}