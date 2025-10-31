namespace Event_Management.Exceptions
{
    // Thrown when a booking cannot be completed due to insufficient available seats.
    public class SeatsUnavailableException : Exception
    {
        public SeatsUnavailableException() : base("The requested number of seats are unavailable.") { }

        public SeatsUnavailableException(string message) : base(message) { }

        public SeatsUnavailableException(string message, Exception inner) : base(message, inner) { }

        public SeatsUnavailableException(int eventId, int requestedSeats, int availableSeats)
            : base($"Event ID {eventId}: Cannot book {requestedSeats} seats. Only {availableSeats} seats available.") { }
    }
}