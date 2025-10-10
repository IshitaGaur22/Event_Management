using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IBookingRepository
    {
        Booking GetBookingById(int id);
        IEnumerable<Booking> GetAllBookings();
        int AddBooking(Booking booking);
        Ticket GetLatestTicket();
        void UpdateTicket(Ticket ticket);
        void UpdateBooking(Booking booking);

    }
}
