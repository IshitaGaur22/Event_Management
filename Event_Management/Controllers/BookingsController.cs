using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsPolicy")]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //Post
        //[Authorize]
        //[HttpPost]
        //public IActionResult BookTickets([FromQuery] int selectedSeats, [FromQuery] int eventId)
        //{
        //    if (selectedSeats <= 0 ||  eventId <= 0)
        //        return BadRequest("Selected seats, and event ID are required.");

        //    try
        //    {
        //        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        if (string.IsNullOrEmpty(userIdClaim)) return Unauthorized("User Id not found in token");
        //        int userId = int.Parse(userIdClaim);
        //        var summary = _bookingService.AddBooking(selectedSeats, userId, eventId);
        //        return Ok(summary);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost]
        public IActionResult BookTickets([FromBody] BookingRequestDto dto)
        {
            if (dto.SelectedSeats <= 0 || dto.EventId <= 0 || dto.UserId <= 0)
                return BadRequest("Selected seats, event ID, and user ID are required.");

            try
            {
                var summary = _bookingService.AddBooking(dto.SelectedSeats, dto.UserId, dto.EventId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPost("SubmitFeedback")]
        //public ActionResult SubmitFeedback([FromBody] CreateFeedbackDto feedback)
        //{
        //    try
        //    {
        //        return Ok(_service.SubmitFeedback(feedback));
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }

        //}

        //Get

        [HttpGet]
        public IActionResult GetAllBookings()
        {
            var bookings = _bookingService.GetAllBookings();
            if(bookings==null || !bookings.Any())
                return NotFound("No bookings found.");
            return Ok(bookings);
        }

        [HttpGet("bookingId")]
        public IActionResult GetBooking([FromQuery] int id)
        {
            var booking = _bookingService.GetBookingById(id);
            if (booking == null)
                return NotFound("No booking is found with this ID");

            return Ok(booking);
        }

        [HttpGet("UserName")]
        public IActionResult GetBookingByName([FromQuery] string name)
        {
            var booking = _bookingService.GetBookingByName(name);
            if (booking == null)
                return NotFound("No booking is found for this Username");
            return Ok(booking);
        }

        [HttpGet("seatAvailability")]
        public IActionResult IsSeatAvailable([FromQuery] int eventId, [FromQuery] int requestedSeats)
        {
            var availableSeats = _bookingService.IsSeatAvailable(eventId, requestedSeats);
            if (availableSeats == null)
                return NotFound("Event not found.");
            return Ok(availableSeats);
        }

        [HttpGet("eventId")]
        public IActionResult GetBookingsByEvent([FromQuery] int eventId)
        {
            var booking = _bookingService.GetBookingsByEvent(eventId);
            if (booking == null)
                return NotFound("No booking is found for this Event ID");
            return Ok(booking);
        }

        [HttpGet("topEvents")]
        public IActionResult GetTopBookedEvents(int count)
        {
            var topEvents = _bookingService.GetTopBookedEvents(count);
            if (topEvents == null || !topEvents.Any())
                return NotFound("No bookings found.");
            return Ok(topEvents);
        }
        [HttpGet("payment/{bookingId}")]
        public IActionResult GetPaymentByBooking(int bookingId)
        {
            var payment = _bookingService.GetPaymentByBookingId(bookingId);

            if (payment == null)
                return NotFound($"No payment found for booking ID {bookingId}.");

            return Ok(payment);
        }

        //Put

        [HttpPut("bookingId")]
        //public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
        //{
        //    var result = _bookingService.UpdateBooking(id, booking);
        //    if (result == 0)
        //        return NotFound("Booking Not Found.");
        //    return Ok("Booking updated succesfully.");
        //}
        public IActionResult UpdateBooking(int id, [FromBody] UpdateBookingDto bookingDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _bookingService.UpdateBooking(id, bookingDto);
                return NoContent(); 
            }
            catch (BookingNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (SeatsUnavailableException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update-completed-bookings")]
        public IActionResult UpdateCompletedBookings()
        {
            var updatedCount = _bookingService.UpdateCompletedBookings();
            return Ok($"{updatedCount} bookings marked as completed.");
        }

        //Delete

        [HttpDelete("id")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _bookingService.DeleteBooking(id);
            if (booking == 0)
                return NotFound("Booking not found.");
            return Ok("Booking deleted successfully.");
        }
    }
}

