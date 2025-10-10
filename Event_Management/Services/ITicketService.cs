using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ITicketService
    {
        //int CreateTicket(Ticket tv);
        void UpdateTicketDetails(Ticket ticket);
        Ticket GetTicketById(int id);
        IEnumerable<Ticket> GetAllTickets();
        void DeleteTicket(int ticketId);
    }
}
