using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IBookingRepository
    {
        Booking GetBookingById(int id);
        IEnumerable<Booking> GetAllBookings();
        int AddBooking(Booking booking);
        Event GetLatestTicket();
        void UpdateTicket(Event ticket);
        User GetUserByUsername(string username);
        void UpdateBooking(Booking booking);

    }
}
