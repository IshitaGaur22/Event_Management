using Event_Management.Models;
using Event_Management.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Event_Management.Exceptions;
using Event_Management.DTOs;

namespace Event_Management.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly Event_ManagementContext _context;
        public BookingRepository(Event_ManagementContext context)
        {
            _context = context;
        }


        //Post
        public int AddBooking(Booking booking)
        {
            _context.Booking.Add(booking);
            return _context.SaveChanges();
        }

        //Get
        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Booking.ToList();
        }

        public Booking GetBookingById(int id)
        {
            return _context.Booking.FirstOrDefault(b => b.BookingId == id);
        }
        public Event GetLatestTicket()
        {
            return _context.Event.OrderByDescending(t => t.EventID).FirstOrDefault();
        }

        public User GetUserById(int userId)
        {
            return _context.User.FirstOrDefault(u => u.UserId == userId);
        }

        public IEnumerable<Booking> GetBookingByName(string username)
        {
            return _context.Booking.Where(b => b.User.UserName == username).ToList();
        }

        public object GetSeats(int id)
        {
            return _context.Event.Where(e=>e.EventID==id).Select(e=> new {e.TotalSeats, e.PricePerTicket }).FirstOrDefault();
        }
        public Payment GetPaymentByBookingId(int bookingId)
        {
            return _context.Payment.FirstOrDefault(p => p.BookingId == bookingId);
        }
        public Event GetEventById(int eventId)
        {
            return _context.Event.FirstOrDefault(e => e.EventID == eventId);
        }

        public IEnumerable<Booking> GetBookingsByEvent(int eventId)
        {
            return _context.Booking.Where(e => e.EventId == eventId).ToList();
        }
        public IEnumerable<Event> GetTopBookedEvents(int count)
        {
            return _context.Event
                    .OrderByDescending(e => _context.Booking.Count(b => b.EventId == e.EventID))
                    .Take(count)
                    .ToList();
        }
        public IEnumerable<Booking> GetPendingBookingsWithEvents()
        {
            return _context.Booking
                .Include(b => b.Event)
                .Where(b => b.Status == "Upcoming")
                .ToList();
        }


        //Put
        public void UpdateTicket(Event ticket)
        {
            _context.Event.Update(ticket);
            _context.SaveChanges();
        }
        //public int UpdateBooking(int id, Booking booking)
        //{
        //    var existing = _context.Booking.Find(id);
        //    if (existing == null)
        //        return 0;
        //    existing.SelectedSeats = booking.SelectedSeats;
        //    existing.BookingDate = booking.BookingDate;
        //    existing.EventId = booking.EventId;
        //    existing.UserId = booking.UserId;

        //    return _context.SaveChanges();
        //}
        public void UpdateBooking(Booking booking)
        {
            _context.Booking.Update(booking);
            _context.SaveChanges();
        }
        void IBookingRepository.UpdateBooking(int id, UpdateBookingDto bookingDto)
        {
            var existingBooking = _context.Booking.Find(id);
            existingBooking.SelectedSeats = bookingDto.SelectedSeats;
            _context.Booking.Update(existingBooking);
            _context.SaveChanges();
        }

        public void UpdateEventSeats(Event ev)
        {
            _context.Event.Update(ev);
            _context.SaveChanges();
        }
        public int SaveUpdatedBookings(IEnumerable<Booking> bookings)
        {
            _context.Booking.UpdateRange(bookings);
            return _context.SaveChanges();
        }


        //Delete
        public int DeleteBooking(int id)
        {
            var booking = _context.Booking.Find(id);
            if (booking == null)
                return 0;
            _context.Booking.Remove(booking);
            return _context.SaveChanges();
        }

        public IEnumerable<Event> GetEventsStartingSoon(int minutes)
        {
            var now = DateTime.Now;
            var threshold = now.AddMinutes(minutes);

            return _context.Event
                .Where(e => e.EventDate == DateOnly.FromDateTime(now) &&
                            e.EventTime >= TimeOnly.FromDateTime(now) &&
                            e.EventTime <= TimeOnly.FromDateTime(threshold))
                .ToList();
        }

        public IEnumerable<User> GetUsersForEvent(int eventId)
        {
            return _context.Booking
                .Where(b => b.EventId == eventId && b.Status == "Confirmed")
                .Select(b => b.User)
                .ToList();
        }

    }
}
