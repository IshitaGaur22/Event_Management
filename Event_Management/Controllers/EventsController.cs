using Event_Management.Data;
using Event_Management.Exceptions;
using Event_Management.Models;
using Event_Management.Services;
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


        [HttpPut("update-name")]
        public IActionResult UpdateEventName([FromQuery] int id, [FromQuery] string? newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return BadRequest("You didn't enter new event name. Please enter it");

            try
            {
                service.UpdateEventName(id, newName);
                return Ok("Event name updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
            }
        }


        [HttpPut("update-description")]
        public IActionResult UpdateEventDescription([FromQuery] int id, [FromQuery] string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return BadRequest("You didn't enter new event description. Please enter it");
            try
            {
                service.UpdateEventDescription(id, description);
                return Ok("Event description updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
            }
        }


        [HttpPut("update-date")]
        public IActionResult UpdateEventDate([FromQuery] int id, [FromQuery] DateOnly? date)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Check the value that you have entered" });

            if (date == null)
                return BadRequest("You didn't enter new event date. Please enter it");

            try
            {
                service.UpdateEventDate(id, date.Value);
                return Ok("Event date updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
            }
        }

       

        [HttpPut("update-time")]
        public IActionResult UpdateEventTime([FromQuery] int id, [FromQuery] TimeOnly? time)
        {

            if (time == null)
                return BadRequest("You didn't enter new event time. Please enter it");

            try
            {
                service.UpdateEventTime(id, time.Value);
                return Ok("Event time updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
            }
        }
        [HttpPut("update-location")]
        public IActionResult UpdateEventLocation([FromQuery] int id, [FromQuery] string? location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("You didn't enter new event location. Please enter it");
            try
            {
                service.UpdateEventLocation(id, location);
                return Ok("Event location updated successfully.");
            }
            catch (EventUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { error = "Check the value that you have entered" });
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
            catch (CategoryNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
        [HttpGet("by-name")]
        public IActionResult GetEvent([FromQuery] string? eventName)
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


        [HttpDelete("eventName")]
        public IActionResult DeleteEvent(string? eventName)
        {
            if (string.IsNullOrWhiteSpace(eventName))
                return BadRequest("You didn't enter new event name. Please enter it");

            service.Delete(eventName);
            return Ok("Availability deleted successfully.");
        }


    }
}