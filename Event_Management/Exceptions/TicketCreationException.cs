namespace Event_Management.Exceptions
{
    public class TicketCreationException:ApplicationException
    {
        public TicketCreationException() { }
        public TicketCreationException(string message):base($"Error occurred while creating ticket: {message}") { }
    }
}
