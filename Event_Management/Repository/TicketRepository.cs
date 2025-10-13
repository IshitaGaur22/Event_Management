using Event_Management.Data;
using Event_Management.Exceptions;
using Event_Management.Models;
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly Event_ManagementContext context;

        public TicketRepository(Event_ManagementContext ctx)
        {
            context = ctx;
        }

        //public int AddTicket(Ticket ticket)
        //{
        //    var evt = context.Ticket.FirstOrDefault(e => e.TicketID == ticket.TicketID);
        //    if (evt != null)
        //    {
        //        return 0;
        //    }
        //    context.Ticket.Add(ticket);
        //    return context.SaveChanges();
        //}

        public void DeleteTicket(int ticketId)
        {
            var ticket = context.Ticket.Find(ticketId);
            if (ticket != null)
            {
                context.Ticket.Remove(ticket);
                context.SaveChanges();
            }
        }

        public Ticket GetTicketById(int ticketId) => context.Ticket.FirstOrDefault(t => t.TicketID == ticketId);

        public IEnumerable<Ticket> GetAllTickets() => context.Ticket.ToList();

        public void UpdateTicketDetails(Ticket ticket)
        {
            var existingTicket = context.Ticket.Find(ticket.TicketID);
            if (existingTicket == null)
                throw new TicketNotFoundException(ticket.TicketID);
            existingTicket.TotalSeats = ticket.TotalSeats;
            existingTicket.PricePerTicket = ticket.PricePerTicket;
            context.SaveChanges();
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
