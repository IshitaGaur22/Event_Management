using Event_Management.Models;
using EventManagement.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Event_Management.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly EventServiceContext _context;
        public BookingRepository(EventServiceContext context)
        {
            _context = context;
        }


        public Booking GetBookingById(int id)
        {
            return _context.Booking
                .Include(b => b.Ticket)
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingId == id);
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Booking
                .Include(b => b.Ticket)
                .Include(b => b.Event)
                .Include(b => b.User)
                .ToList();
        }

        public int AddBooking(Booking booking)
        {
            _context.Booking.Add(booking);
            return _context.SaveChanges();
        }

        public Ticket GetLatestTicket()
        {
            return _context.Ticket.OrderByDescending(t => t.TicketId).FirstOrDefault();
        }

        public void UpdateTicket(Ticket ticket)
        {
            _context.Ticket.Update(ticket);
            _context.SaveChanges();
        }



    }
}
