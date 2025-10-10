namespace Event_Management.Exceptions
{
    public class TicketDeletionException:ApplicationException
    {
        public TicketDeletionException() { }
        public TicketDeletionException(int ticketID) : base($"Ticket with ID {ticketID} could not be deleted.") { }
    }
}
