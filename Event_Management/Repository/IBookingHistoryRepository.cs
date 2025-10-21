using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IBookingHistoryRepository
    {
        Task<List<BookingHistoryDTO>> GetUpcomingBookings(int userId);
        Task<List<BookingHistoryDTO>> GetPastBookings(int userId);
        Task<List<BookingHistoryDTO>> SearchByEventName(int userId, string eventName);
        Task<List<BookingHistoryDTO>> SearchByDate(int userId, DateOnly date);
        
        Task<Booking> GetBookingWithEvent(int bookingId);

        Task SaveChangesAsync();
    }
}
