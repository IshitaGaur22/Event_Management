using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Repository;
using Event_Management.Data;

namespace Event_Management.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IPaymentRepository _paymentRepository;

        public BookingService(IBookingRepository repo, IPaymentRepository paymentRepository)
        {
            _bookingRepository = repo;
            _paymentRepository = paymentRepository;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAllBookings();
        }

        public BookingSummary AddBooking(int selectedSeats, string userName)
        {
            var ticket = _bookingRepository.GetLatestTicket();
            if (ticket == null || selectedSeats > ticket.TotalSeats)
                throw new Exception("Invalid ticket or seat count.");

            var user = _bookingRepository.GetUserByUsername(userName);
            if(user == null)
                throw new Exception("User not found.");
            var booking = new Booking
            {
                TicketId = ticket.TicketID,
                EventId = ticket.EventId,
                UserId = user.UserId,
                SelectedSeats = selectedSeats,
                BookingDate = DateTime.Now,
                Status = "Pending"
            };

            ticket.TotalSeats -= selectedSeats;
            _bookingRepository.UpdateTicket(ticket);
            _bookingRepository.AddBooking(booking);

            decimal amount= ticket.PricePerTicket * selectedSeats;
            var payment= new Payment
            {
                TicketId= ticket.TicketID,
                BookingId = booking.BookingId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Credit Card", // Example method
                Status = "Completed" // Assuming payment is successful
            };
            _paymentRepository.AddPayment(payment);
            return new BookingSummary
            {

                EventName = ticket.Event.EventName,
                Location = ticket.Event.Location,
                //EventDate = ticket.Event.EventDate,
                Time = ticket.Event.EventTime,
                PricePerTicket = ticket.PricePerTicket,
                SelectedSeats = selectedSeats,
                TotalAmount = amount
            };
        }

        public Booking GetBookingById(int id)
        {
            return _bookingRepository.GetBookingById(id);
        }

        public void UpdateBooking(Booking booking)
        {
            _bookingRepository.UpdateBooking(booking);
        }
    }
}
