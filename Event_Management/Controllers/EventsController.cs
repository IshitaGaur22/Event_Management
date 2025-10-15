using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
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
    [FromQuery] DateOnly? date,
    [FromQuery] TimeOnly? time,
    [FromQuery] string? location)
        {
            if (name == null && description == null && date == null && time == null && location == null)
                return BadRequest("No fields provided to update.");

            try
            {
                service.UpdateEvent(id, name, description, date, time, location);
                return Ok("Event updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });

            }
            catch(EventsNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the values you have entered." });
            }
        }

        [HttpGet("total-Events")]
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
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        public IActionResult GetAllEvents()
        {
            try
            {
                var c = service.GetAllEvents();
                return Ok(c);
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
                var ev=service.FetchEventName(eventName);
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
            return Ok(ev);
        }

        [HttpGet("by-date")]
        public IActionResult GetEventByDate([FromQuery] DateOnly? date)
        {
            if (date == null)
                return BadRequest("You didn't enter new event date. Please enter it");

            var ev = service.FetchEventDate(date.Value);
            return Ok(ev);
        }

        [HttpGet("tickets")]
        [ProducesResponseType(typeof(IEnumerable<Event>), 200)]
        public IActionResult GetAllTickets()
        {
            var Ticket = service.GetAllTickets();
            return Ok(Ticket);
        }

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