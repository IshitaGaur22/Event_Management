namespace Event_Management.Exceptions
{
    public class TicketNotFoundException:ApplicationException
    {
       
        public TicketNotFoundException(int ticketID):base($"Event with ID {ticketID} was not found") { }
    }
}
