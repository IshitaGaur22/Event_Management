using Event_Management.DTOs;
using Event_Management.ExceptionHandlers;
using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ExceptionHandler] // to handle exceptions globally for this controller
    public class EventsController : ControllerBase //features required to run a dotnet application
    {
        private readonly IEventService service;

        public EventsController(IEventService eventService)
        {
            service = eventService;
        }

        [HttpPost]
        public IActionResult CreateEvent(Event events)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid model state.", details = ModelState });
            if (events == null)
                return BadRequest("No values entered, please enter values.");


            try
            {
                service.CreateEvent(events);
                return StatusCode(201, new { message = "Event created successfully." });
            }
            catch (CategoryNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (EventAlreadyExistsException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (EventCreationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        [HttpPut("update-event")]
        public IActionResult UpdateEvent(
        [FromQuery] int id,
        [FromQuery] string? name,
        [FromQuery] string? description,
        [FromQuery] string? location,
        [FromQuery] int TotalSeats,
        [FromQuery] decimal PricePerTicket,
        [FromQuery] DateOnly? date,
        [FromQuery] TimeOnly? time,

        [FromQuery] TimeOnly? endTime
        )
        {

            //if (name == null && description == null && date == null && time == null && location == null)
            //    return BadRequest("No fields provided to update.");

            if (string.IsNullOrWhiteSpace(name) &&
    string.IsNullOrWhiteSpace(description) &&
    string.IsNullOrWhiteSpace(location) &&
    TotalSeats <= 0 &&
    PricePerTicket <= 0 &&
    date == null &&
    time == null &&
    endTime == null)
            {
                return BadRequest("No fields provided to update.");
            }


            try
            {
                service.UpdateEvent(id, name, description, location, TotalSeats, PricePerTicket, date, time, endTime);
                return Ok("Event updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });

            }
            catch (EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the values you have entered." });
            }
        }

        [HttpGet("Total Number Of Events")]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        public IActionResult GetTotalNumberOfEvents()
        {
            try
            {
                var c = service.GetTotalEvents();
                return Ok(c);
            }
            catch (EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("Total Number Of Bookings")]
        [ProducesResponseType(typeof(IEnumerable<Booking>), 200)]
        public IActionResult GetTotalBookings()
        {
            try
            {
                var totalBookings = service.GetTotalBookings();
                return Ok(totalBookings);
            }
            catch (BookingNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
        [HttpGet("Total Revenue Generated")]
        [ProducesResponseType(typeof(IEnumerable<Payment>), 200)]
        public IActionResult GetTotalRevenue()
        {
            try
            {
                var totalRevenue = service.GetTotalRevenue();
                return Ok(totalRevenue);
            }
            catch (BookingNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
        [HttpGet("Total Number Of Users")]
        [ProducesResponseType(typeof(IEnumerable<Payment>), 200)]
        public IActionResult GetTotalNoOfUsers()
        {
            //try
            //{
            var totalUserCount = service.GetTotalNoOfUsers();
            return Ok(totalUserCount);
            //}
            //catch(UsersNotFoundException ex)
            //{
            //    return NotFound(new { error = ex.Message });
            //}
        }



        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        public IActionResult GetAllEvents()
        {
            try
            {
                var events = service.GetAllEvents();
                if (!events.Any())
                {
                    return Ok(new { message = "No Events Found", events = new List<Event>() });
                }
                return Ok(events);
            }
            catch (EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }




        [HttpGet("by-name")]
        public IActionResult GetEventByName([FromQuery] string? eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return BadRequest("You didn't enter new event name. Please enter it");
            try
            {
                var ev = service.FetchEventName(eventName);

                return Ok(ev);
            }
            catch (EventsNotFoundException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
            }
        }
        [HttpGet("by-location")]
        public IActionResult GetEventByLocation([FromQuery] string? location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("You didn't enter new event location. Please enter it");
            var ev = service.FetchEventLocation(location);
            if (!ev.Any())
            {
                return Ok($"Event with Location {location} doesn't exist ");
            }
            return Ok(ev);


        }

        [HttpGet("by-date")]
        public IActionResult GetEventByDate([FromQuery] DateOnly? date)
        {
            if (date == null)
                return BadRequest("You didn't enter new event date. Please enter it");

            var ev = service.FetchEventDate(date.Value);
            if (!ev.Any())
            {
                return Ok($"Event with Date {date} doesn't exist ");
            }
            return Ok(ev);
        }

        //[HttpGet("tickets")]
        //[ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        //public IActionResult GetAllTickets()
        //{
        //    var Ticket = service.GetAllTickets();
        //    return Ok(Ticket);
        //}

        [HttpGet("{id}")]
        public IActionResult GetEventById(int? id)
        {
            if (id == null)
                return BadRequest("Event ID is required.Please enter it");
            try
            {
                var ticket = service.GetEventbyId(id.Value);
                return Ok(ticket);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet("Event Summary Report")]
        [ProducesResponseType(typeof(IEnumerable<EventRevenueDto>), 200)]
        public IActionResult GetEventRevenueSummary()
        {
            try
            {
                var eventSummary = service.GetEventRevenueSummary();
                return Ok(eventSummary);
            }
            catch (EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }


        [HttpDelete("eventName")]
        public IActionResult DeleteEvent(string? eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return BadRequest("You didn't enter new event name. Please enter it");


            try
            {
                service.Delete(eventName);
                return Ok($"{eventName} Event deleted successfully.");

            }
            catch (EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }


    }
}