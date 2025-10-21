using Event_Management.Models;
using Event_Management.Services;
using Event_Management.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {

        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        //Post
        [HttpPost]
        public IActionResult BookTickets([FromQuery] int selectedSeats, [FromQuery] string userName, [FromQuery] int eventId)
        {
            if (selectedSeats <= 0 || string.IsNullOrEmpty(userName) || eventId <= 0)
                return BadRequest("Selected seats, username, and event ID are required.");

            try
            {
                var summary = _bookingService.AddBooking(selectedSeats, userName, eventId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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

        //Put

        [HttpPut("bookingId")]
        public IActionResult UpdateBooking(int id, [FromBody] Booking booking)
        {
            var result = _bookingService.UpdateBooking(id, booking);
            if (result == 0)
                return NotFound("Booking Not Found.");
            return Ok("Booking updated succesfully.");
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

