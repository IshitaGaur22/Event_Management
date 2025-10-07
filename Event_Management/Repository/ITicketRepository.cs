using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface ITicketRepository
    {

        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        Task AddTicketAsync(Ticket ticket);
        Task UpdateTicketAsync(Ticket ticket);
        Task SaveChangesAsync();

    }
}
