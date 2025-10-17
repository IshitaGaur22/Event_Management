using Event_Management.Models;
using Event_Management.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Event_Management.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Event_ManagementContext _context;
        public BookingRepository(Event_ManagementContext context)
        {
            _context = context;
        }


        public Booking GetBookingById(int id)
        {
            return _context.Booking
                //.Include(b => b.Ticket)
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingId == id);
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Booking.ToList();
        }

        public int AddBooking(Booking booking)
        {
            _context.Booking.Add(booking);
            return _context.SaveChanges();
        }

        public Event GetLatestTicket()
        {
            return _context.Event.OrderByDescending(t => t.EventID).FirstOrDefault();
        }

        public void UpdateTicket(Event ticket)
        {
            _context.Event.Update(ticket);
            _context.SaveChanges();
        }

        public User GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(u => u.UserName == username);
        }

        public void UpdateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
