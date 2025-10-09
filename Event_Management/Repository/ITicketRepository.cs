using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface ITicketRepository
    {
        //public int AddTicket(Ticket ticket);
        public Ticket GetTicketById(int ticketId);
        public IEnumerable<Ticket> GetAllTickets();
        public void UpdateTicketDetails(Ticket ticket);
        
        public void DeleteTicket(int ticketId);

    }
}
