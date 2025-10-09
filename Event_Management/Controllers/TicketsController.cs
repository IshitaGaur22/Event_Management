using Event_Management.Data;
using Event_Management.Models;
using Event_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Event_Management.Exceptions;
using Event_Management.ExceptionHandlers;


namespace Event_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _service;

        public TicketsController(ITicketService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult GetTicketById(int id)
        {
            try
            {
                var ticket = _service.GetTicketById(id);
                return Ok(ticket);
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Ticket>), 200)]
        public IActionResult GetAllTickets()
        {
            var Ticket = _service.GetAllTickets();
            return Ok(Ticket);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTicketDetails(int id, [FromBody] Ticket ticket)
        {
            if (id != ticket.TicketID)
                return BadRequest(new { error = $"ticket ID mismatch. Route ID: {id}, Body ID: {ticket.TicketID}" });

            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid model state.", details = ModelState });

            try
            {
                _service.UpdateTicketDetails(ticket); 
                return Ok(new { message = $"ticket with ID {id} updated successfully." });
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (TicketUpdateException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Unexpected error occurred.", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTicket(int id)
        {
            try
            {
                _service.DeleteTicket(id);
                return Ok(new { message = $"ticket with ID {id} deleted successfully." });
            }
            catch (TicketNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (TicketDeletionException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        
    }
}
