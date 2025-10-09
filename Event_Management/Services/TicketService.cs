using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Repository;

namespace Event_Management.Services
{

    public class TicketService : ITicketService
    {
        private readonly ITicketRepository repository;

        public TicketService(ITicketRepository repo)
        {
            repository = repo;
        }
        //public int CreateTicket(Ticket tv)
        //{
        //    if (repository.GetTicketById(tv.TicketID) != null)
        //        throw new TicketAlreadyExistsException(tv.TicketID); 

        //    try
        //    {
        //        return repository.AddTicket(tv);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new TicketCreationException(ex.Message);
        //    }

        //}
        public void UpdateTicketDetails(Ticket ticket)
        {
            if (repository.GetTicketById(ticket.TicketID) == null)
                throw new TicketNotFoundException(ticket.TicketID);
            try
            {
                repository.UpdateTicketDetails(ticket);
            }
            catch 
            {
                throw new TicketUpdateException(ticket.TicketID);
            }
        }

        public Ticket GetTicketById(int id)
        {
            var ticket = repository.GetTicketById(id);
            if (ticket == null)
                throw new TicketNotFoundException(id);
            return ticket;
        }
        public IEnumerable<Ticket> GetAllTickets() => repository.GetAllTickets();
        public void DeleteTicket(int ticketId)
        {
            if (repository.GetTicketById(ticketId) == null)
                throw new TicketNotFoundException(ticketId);
            try
            {
                repository.DeleteTicket(ticketId);
            }
            catch 
            {
                throw new TicketDeletionException(ticketId);
            }
        }
    }
}

