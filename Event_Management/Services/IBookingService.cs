using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IBookingService
    {

        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        BookingSummary AddBooking(int selectedSeats, string userName);
        public void UpdateBooking(Booking booking);

    }
}
