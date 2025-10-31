namespace Event_Management.Exceptions
{
    // Thrown when a specific booking record is not found.
    public class BookingNotFoundException : Exception
    {
        public BookingNotFoundException() : base("Booking not found.") { }

        public BookingNotFoundException(string message) : base(message) { }

        public BookingNotFoundException(string message, Exception inner) : base(message, inner) { }

        public BookingNotFoundException(int bookingId) : base($"Booking with ID {bookingId} not found.") { }
    }
}