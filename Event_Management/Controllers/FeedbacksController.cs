using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Services;
using EventFeedback.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("MyCorsPolicy")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbacksController(IFeedbackService service)
        {
            _service = service;
        }
        [HttpGet("ViewAllFeedbacks")]
        public IActionResult GetFeedback()
        {
            return Ok(_service.GetFeedback());
        }

        
        [HttpPost("SubmitFeedback")]
        public ActionResult SubmitFeedback([FromBody] CreateFeedbackDto feedback)
        {
            try
            {
                return Ok(_service.SubmitFeedback(feedback));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("GetBookedEventsByUserId/{userId}")]
        public ActionResult<object> GetBookedEventsByUserId(int userId)
        {
            try
            {
                var events = _service.GetBookedEventsForUser(userId);

                if (events == null || !events.Any())
                    return NotFound("No booked events found for this user.");
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetFeedbackSummary/{eventId}")]
        public ActionResult<object> GetFeedbackSummary(int eventId)
        {
            try
            {
                var sum = _service.GetFeedbackSummary(eventId);
                return Ok(sum);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TopRatedEvents")]
        public IActionResult GetTopRatedEvents()
        {
            try
            {
                var topRatedEvents = _service.GetTopRatedEvents();
                return Ok(topRatedEvents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("FilterFeedbacks")]

        public ActionResult<List<Feedback>> GetFilteredFeedbacks(
        string? eventName,
        int? minRating,
        DateTime? startDate,
        DateTime? endDate,
        string? search,
        SortByOptions sortBy = SortByOptions.SubmittedAt,
        SortOrderOptions sortOrder = SortOrderOptions.descending)
        {
            try
            {
                var result = _service.GetFilteredFeedbacks(
                    eventName, minRating, startDate, endDate, search, sortBy, sortOrder);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPost("ReplyToFeedback/{feedbackId}")]
        public IActionResult ReplyToFeedback(int feedbackId, ReplyDto reply)
        {
            try
            {
                _service.SubmitReply(feedbackId, reply);
                return Ok("Reply submitted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("ArchiveFeedback/{feedbackId}")]
        public IActionResult ArchiveFeedback(int feedbackId)
        {
            try
            {
                _service.ArchiveFeedback(feedbackId);
                return Ok("Feedback Archived");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
        [HttpPut("UnArchiveFeedback/{feedbackId}")]
        public IActionResult UnArchiveFeedback(int feedbackId)
        {
            try
            {
                _service.UnArchiveFeedback(feedbackId);
                return Ok("Feedback Unarchived");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
