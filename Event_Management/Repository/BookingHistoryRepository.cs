using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Data;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class BookingHistoryRepository : IBookingHistoryRepository
    {
        private readonly Event_ManagementContext _context;

        public BookingHistoryRepository(Event_ManagementContext context)
        {
            _context = context;
        }

        public async Task<List<BookingHistoryDTO>> GetUpcomingBookings(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Booking
                .Include(b => b.Event)
                .Where(b => b.UserId == userId && b.Event.EventDate >= today)
                .Select(b => new BookingHistoryDTO
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    Location = b.Event.Location,
                    EventDate = b.Event.EventDate,
                    EventTime = b.Event.EventTime,
                    SelectedSeats = b.SelectedSeats,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<List<BookingHistoryDTO>> GetPastBookings(int userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Booking
                .Include(b => b.Event)
                .Where(b => b.UserId == userId && b.Event.EventDate < today)
                .Select(b => new BookingHistoryDTO
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    Location = b.Event.Location,
                    EventDate = b.Event.EventDate,
                    EventTime = b.Event.EventTime,
                    SelectedSeats = b.SelectedSeats,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<List<BookingHistoryDTO>> SearchByEventName(int userId, string eventName)
        {
            return await _context.Booking
                .Include(b => b.Event)
                .Where(b => b.UserId == userId && b.Event.EventName.ToLower().Contains(eventName.ToLower()))
                .Select(b => new BookingHistoryDTO
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    Location = b.Event.Location,
                    EventDate = b.Event.EventDate,
                    EventTime = b.Event.EventTime,
                    SelectedSeats = b.SelectedSeats,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<List<BookingHistoryDTO>> SearchByDate(int userId, DateOnly date)
        {
            return await _context.Booking
                .Include(b => b.Event)
                .Where(b => b.UserId == userId && b.Event.EventDate == date)
                .Select(b => new BookingHistoryDTO
                {
                    BookingId = b.BookingId,
                    EventName = b.Event.EventName,
                    Location = b.Event.Location,
                    EventDate = b.Event.EventDate,
                    EventTime = b.Event.EventTime,
                    SelectedSeats = b.SelectedSeats,
                    Status = b.Status
                }).ToListAsync();
        }

        public async Task<Booking> GetBookingWithEvent(int bookingId)
        {
            return await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.User) // ✅ Add this line
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);
        }



        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
