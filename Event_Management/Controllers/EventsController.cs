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
            if(events==null)
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
//        try
//            {
//                service.UpdateEventName(id, newName);
//                return Ok("Event name updated successfully.");
//    }
//            catch(EventUpdateException ex)
//            {
//                throw new EventUpdateException(newName);
//}

        [HttpPut("update-name")]
        public IActionResult UpdateEventName([FromQuery] int id, [FromQuery] string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                return BadRequest("You didn't enter new event name. Please enter it");

            service.UpdateEventName(id, newName);
            return Ok("Event name updated successfully.");
        }

        [HttpPut("update-description")]
        public IActionResult UpdateEventDescription([FromQuery]int id,[FromQuery] string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return BadRequest("You didn't enter new event description. Please enter it");
            service.UpdateEventDescription(id, description);
            return Ok("Event description updated successfully.");
        }


        [HttpPut("update-date")]
        public IActionResult UpdateEventDate([FromQuery] int id, [FromQuery] DateOnly date)
        {
            if (string.IsNullOrEmpty(date.ToString()))
                return BadRequest("You didn't enter new event date. Please enter it");

            service.UpdateEventDate(id, date);
            return Ok("Event date updated successfully.");
        }

        [HttpPut("update-time")]
        public IActionResult UpdateEventTime([FromQuery] int id, [FromQuery] TimeOnly time)
        {
            if (string.IsNullOrWhiteSpace(time.ToString()))
                return BadRequest("You didn't enter new event time. Please enter it");

            service.UpdateEventTime(id, time);
            return Ok("Event time updated successfully.");
        }

        [HttpPut("update-location")]
        public IActionResult UpdateEventLocation([FromQuery] int id, [FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("You didn't enter new event location. Please enter it");

            service.UpdateEventLocation(id, location);
            return Ok("Event location updated successfully.");
        }


        [HttpGet("by-name")]
        public IActionResult GetEvent([FromQuery] string eventName)
        {
            if (eventName.Length==0)
                return BadRequest("You didn't enter new event name. Please enter it");

            var ev = service.FetchEventName(eventName);
            return Ok(ev);
        }

        [HttpGet("by-location")]
        public IActionResult GetEventByLocation([FromQuery] string location)
        {
            if (location.Length==0)
                return BadRequest("You didn't enter new event location. Please enter it");

            var ev = service.FetchEventLocation(location);
            return Ok(ev);
        }

        [HttpGet("by-date")]
        public IActionResult GetEventByDate([FromQuery] DateOnly date)
        {
            if (date.ToString().Length == 0)
                return BadRequest("You didn't enter new event date. Please enter it");

            var ev = service.FetchEventDate(date);
            return Ok(ev);
        }


        [HttpDelete("eventName")]
        public IActionResult DeleteEvent(string eventName)
        {
            if (eventName == null)
                return BadRequest("You didn't enter new event name. Please enter it");

            service.Delete(eventName);
            return Ok("Availability deleted successfully.");
        }


    }
}
