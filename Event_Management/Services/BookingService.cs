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
        private readonly IEmailService _emailService;

        public BookingService(IBookingRepository repo, IPaymentRepository paymentRepository, IEmailService emailService)
        {
            _bookingRepository = repo;
            _paymentRepository = paymentRepository;
            _emailService = emailService;
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
            if (user == null)
                throw new Exception("User not found.");

            var booking = new Booking
            {
                EventId = ticket.EventID,
                UserId = user.UserId,
                SelectedSeats = selectedSeats,
                BookingDate = DateTime.Now,
                Status = "Confirmed"
            };

            ticket.TotalSeats -= selectedSeats;
            _bookingRepository.UpdateTicket(ticket);
            _bookingRepository.AddBooking(booking);

            decimal amount = ticket.PricePerTicket * selectedSeats;
            var payment = new Payment
            {
                EventID = ticket.EventID,
                BookingId = booking.BookingId,
                Amount = amount,
                PaymentDate = DateTime.Now,
                PaymentMethod = "Credit Card",
                Status = "Completed"
            };
            _paymentRepository.AddPayment(payment);

            // ✅ Send Confirmation Email
            var subject = "Booking Confirmation";
            var body = $"Hi {user.UserName},\n\nYour booking for '{ticket.EventName}' on {ticket.EventDate} at {ticket.EventTime} is confirmed.\n\nThank you!";
            _emailService.SendEmailAsync(user.Email, subject, body);

            return new BookingSummary
            {
                EventName = ticket.EventName,
                Location = ticket.Location,
                Time = ticket.EventTime,
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
