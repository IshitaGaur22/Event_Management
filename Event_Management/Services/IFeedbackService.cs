using Event_Management.Models;
using Event_Management.DTOs;
namespace Event_Management.Services
{
    public interface IFeedbackService
    {
        public List<Feedback> GetFeedback();
        public Feedback GetFeedbackById(int id);
        public int SubmitFeedback(CreateFeedbackDto feedback);
        IEnumerable<object> GetTopRatedEvents();
        object GetFeedbackSummary(int eventId);
        public IEnumerable<object> GetFilteredFeedbacks(
                    string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search,
                    string sortBy,
                    string sortOrder);
        public int SubmitReply(int feedbackId, ReplyDto reply);
        public int ArchiveFeedback(int feedbackId);
        public int UnArchiveFeedback(int feedbackId);
    }
}
