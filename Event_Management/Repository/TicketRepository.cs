using Event_Management.Models;
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class TicketRepository : ITicketRepository
    {

        private readonly EventServiceContext _context;

        public TicketRepository(EventServiceContext context)
        {
            _context = context;
        }

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Ticket.FirstOrDefaultAsync(t => t.TicketId == ticketId);
        }

        Task ITicketRepository.AddTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Ticket>> ITicketRepository.GetAllTicketsAsync()
        {
            throw new NotImplementedException();
        }

        Task ITicketRepository.SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        Task ITicketRepository.UpdateTicketAsync(Ticket ticket)
        {
            throw new NotImplementedException();
        }
    }
}
