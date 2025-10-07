using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IBookingService
    {

        IEnumerable<Booking> GetAllBookings();
        Booking GetBookingById(int id);
        int AddBooking(int selectedSeats);
    }
}
