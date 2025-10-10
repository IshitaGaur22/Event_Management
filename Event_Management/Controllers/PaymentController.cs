using Microsoft.AspNetCore.Mvc;
using Event_Management.Services;
using System.Linq;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public PaymentController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("confirm")]
        public IActionResult ConfirmPayment(
            [FromQuery] int bookingId,
            [FromQuery] string paymentMethod, // "Credit", "Debit", "UPI"
            [FromQuery] string paymentDetails // card number or UPI ID
        )
        {
            var booking = _bookingService.GetBookingById(bookingId);
            if (booking == null) return NotFound("Booking not found.");

            bool isValid = paymentMethod.ToLower() switch
            {
                "credit" => IsValidCard(paymentDetails),
                "debit" => IsValidCard(paymentDetails),
                "upi" => IsValidUpi(paymentDetails),
                _ => false
            };

            if (!isValid)
                return BadRequest("Invalid payment details for selected method.");

            booking.Status = "Confirmed";
            booking.Ticket.TotalSeats -= booking.SelectedSeats;

            _bookingService.UpdateBooking(booking);
            return Ok("Payment successful. Booking confirmed.");
        }

        private bool IsValidCard(string cardNumber)
        {
            // Basic card validation: 16 digits
            return cardNumber.Length == 16 && cardNumber.All(char.IsDigit);
        }

        private bool IsValidUpi(string upiId)
        {
            // Basic UPI validation: must contain '@' and end with common UPI suffix
            return upiId.Contains('@') &&
                   (upiId.EndsWith("@upi") || upiId.EndsWith("@ybl") || upiId.EndsWith("@paytm"));
        }
    }
}