namespace Event_Management.Exceptions
{
    public class TicketUpdateException:ApplicationException
    {
        public TicketUpdateException(int ticketID) : base($"Ticket with ID {ticketID} could not be updated.") { }
    }

}
