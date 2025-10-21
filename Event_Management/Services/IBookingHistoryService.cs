using Event_Management.DTOs;

namespace Event_Management.Services
{
    public interface IBookingHistoryService
    {
        Task<List<BookingHistoryDTO>> GetUpcomingBookings(int userId);
        Task<List<BookingHistoryDTO>> GetPastBookings(int userId);
        Task<List<BookingHistoryDTO>> SearchByEventName(int userId, string eventName);
        Task<List<BookingHistoryDTO>> SearchByDate(int userId, DateOnly date);



        Task CancelBooking(int bookingId);
    }
}
