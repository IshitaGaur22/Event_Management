using Event_Management.Models;

namespace Event_Management.Services
{
    public interface ITicketService
    {

        Task<Ticket> GetTicketAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task<bool> ReduceSeatsAsync(int ticketId, int quantity);

    }
}
