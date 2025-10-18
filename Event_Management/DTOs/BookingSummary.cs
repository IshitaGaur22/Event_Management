namespace Event_Management.DTOs
{
    public class BookingSummary
    {

        public string EventName { get; set; }
        public string Location { get; set; }
        public DateOnly EventDate { get; set; }
        public TimeOnly Time { get; set; }
        public decimal PricePerTicket { get; set; }
        public int SelectedSeats { get; set; }
        //public decimal BaseAmount { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
