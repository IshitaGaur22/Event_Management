using Event_Management.DTOs;
using Event_Management.Exceptions;
using Event_Management.Migrations;
using Event_Management.Models;
using Event_Management.Repository;
using EventFeedback.Exceptions;
using Microsoft.EntityFrameworkCore;
namespace Event_Management.Services
{
    //services class is made to see if it is exception where to go and if not where to go
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;
        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }
        public List<Feedback> GetFeedback()
        {
            return _repository.GetAllFeedbacks();
        }
        public Feedback GetFeedbackById(int id)
        {
            return _repository.GetFeedbackById(id);
        }
        
        public int SubmitFeedback(CreateFeedbackDto feedback)
        {
            var hasAttended = _repository.HasUserAttendedEvent(feedback.UserId, feedback.EventId);
            if (_repository.HasUserAlreadySubmittedFeedback(feedback.UserId, feedback.EventId))
            {
                throw new FeedbackAlreadyExists($"You have already submitted feedback for this event");
            }
            if (!hasAttended)
            {
                throw new InvalidOperationException("Feedback is available only after attending the event.");
            }

            return _repository.SubmitFeedback(feedback);

        }

        public IEnumerable<object> GetTopRatedEvents()
        {
            var topEvents = _repository.GetTopRatedEvents();
            if (topEvents == null || !topEvents.Any())
            {
                throw new FeedbackNotFound("No events with sufficient feedback found.");
            }

            return topEvents;
        }
        public object GetFeedbackSummary(int eventId)
        {
            return _repository.GetFeedbackSummary(eventId);
        }
        public IEnumerable<object> GetFilteredFeedbacks(
                    string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search,
                    string sortBy,
                    string sortOrder)
        {
            var feedbackList = _repository.GetFilteredFeedbacks(eventName, minRating, startDate, endDate, search);

            if (feedbackList == null)
            {
                throw new FeedbackNotFound("No feedbacks found for the given filters.");
            }

            return feedbackList;
        }


        public int SubmitReply(int feedbackId, ReplyDto reply)
        {
            var feedback = _repository.GetFeedbackById(feedbackId);
            if (feedback == null)
                throw new FeedbackNotFound($"Feedback with ID {feedbackId} not found.");
            if (_repository.GetReplyById(feedbackId)!=null)
            {
                throw new ReplyAlreadyExists($"You have already submitted reply for this feedback");
            }
            return _repository.AddReply(feedbackId,reply);
            
        }
        public int ArchiveFeedback(int feedbackId)
        {
            var feedback = _repository.FindFeedbackById_Admin(feedbackId);
            if (feedback == null)
            {
                throw new FeedbackNotFound($"Feedback with ID {feedbackId} not found.");
            }
            if (feedback.IsArchived) 
            {
                throw new FeedbackAlreadyArchivedException("This feedback is already archived.");
            }
            return _repository.ArchiveFeedback(feedbackId);
        }

        public int UnArchiveFeedback(int feedbackId)
        {
            var feedback = _repository.FindFeedbackById_Admin(feedbackId);
            if (feedback == null)
            {
                throw new FeedbackNotFound($"Feedback with ID {feedbackId} not found.");
            }
            if (!feedback.IsArchived) 
            {
                throw new FeedbackNotArchivedException("This feedback is already present.");
            }
            return _repository.UnArchiveFeedback(feedbackId);
        }
    }
}
