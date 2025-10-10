namespace Event_Management.Exceptions
{
    public class TicketNotFoundException:ApplicationException
    {
       
        public TicketNotFoundException(int ticketID):base($"Ticket with ID {ticketID} was not found") { }
    }
}
