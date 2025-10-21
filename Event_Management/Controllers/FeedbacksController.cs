using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Models;
using Event_Management.Services;
using EventFeedback.Exceptions;
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
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbacksController(IFeedbackService service)
        {
            _service = service;
        }
        [HttpGet("View all Feedbacks")]
        public IActionResult GetFeedback()
        {
            return Ok(_service.GetFeedback());
        }


        [HttpPost("SubmitFeedback")]
        public ActionResult SubmitFeedback(CreateFeedbackDto feedback)
        {
            try
            {
                return StatusCode(201, _service.SubmitFeedback(feedback)); 
            }
            catch(InvalidOperationException ex)
            {
                return StatusCode(500,ex.Message);
            }
            catch(FeedbackNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
                return StatusCode(500, e.Message);
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
                return StatusCode(500, ex.Message);
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
                    eventName, minRating, startDate, endDate, search, sortBy.ToString(), sortOrder.ToString());
                return Ok(result);
            }
            catch (FeedbackNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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

            catch (FeedbackNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (FeedbackNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (FeedbackAlreadyArchivedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (FeedbackNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch (FeedbackNotArchivedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
