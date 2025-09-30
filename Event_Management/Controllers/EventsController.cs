using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Event_Management.Models;
using Event_Management.Services;

namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventService.GetAllEvents());
        }

        [HttpPost]
        public ActionResult PostEvent(Event e)
        {
            return StatusCode(201, _eventService.AddEvent(e));
        }

        [HttpDelete]
        public IActionResult DeleteEvent(int id)
        {
            return Ok(_eventService.DeleteEvent(id));
        }

        [HttpPut]
        public IActionResult PutEvent(int id, Event e)
        {
            return Ok(_eventService.UpdateEvent(id, e));

        }
        //private readonly EventServiceContext _context;

        //public EventsController(Event_ManagementContext context)
        //{
        //    _context = context;
        //}

        //// GET: api/Events
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        //{
        //    return await _context.Event.ToListAsync();
        //}

        //// GET: api/Events/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Event>> GetEvent(int id)
        //{
        //    var @event = await _context.Event.FindAsync(id);

        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return @event;
        //}

        //// PUT: api/Events/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutEvent(int id, Event @event)
        //{
        //    if (id != @event.EventID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(@event).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EventExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Events
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Event>> PostEvent(Event @event)
        //{
        //    _context.Event.Add(@event);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetEvent", new { id = @event.EventID }, @event);
        //}

        //// DELETE: api/Events/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteEvent(int id)
        //{
        //    var @event = await _context.Event.FindAsync(id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Event.Remove(@event);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool EventExists(int id)
        //{
        //    return _context.Event.Any(e => e.EventID == id);
        //}
    }
}
