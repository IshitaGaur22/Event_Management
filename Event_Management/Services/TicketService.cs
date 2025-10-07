using Event_Management.Models;
using Event_Management.Repository;

namespace Event_Management.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepo;
        public TicketService(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }


        public async Task<Ticket> GetTicketAsync(int ticketId)
        {
            return await _ticketRepo.GetTicketByIdAsync(ticketId);
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
        {
            return await _ticketRepo.GetAllTicketsAsync();
        }

        public async Task<bool> ReduceSeatsAsync(int ticketId, int quantity)
        {

            var ticket = await _ticketRepo.GetTicketByIdAsync(ticketId);
            if (ticket == null || quantity <= 0 || quantity > ticket.TotalSeats)
                return false;

            ticket.TotalSeats -= quantity;
            await _ticketRepo.UpdateTicketAsync(ticket);
            await _ticketRepo.SaveChangesAsync();
            return true;


        }
    }
}
