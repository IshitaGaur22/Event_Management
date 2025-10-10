namespace Event_Management.Exceptions
{
    public class TicketAlreadyExistsException:ApplicationException
    {
        public TicketAlreadyExistsException(int ticketID) : base($"Ticket with ID {ticketID} already exists.") { }
    }
}
