using Event_Management.Models;

namespace Event_Management.Services
{
    public interface IFeedbackService
    {
        public List<Feedback> GetFeedback();
        public Feedback GetFeedbackById(int id);
        public int UpdateFeedback(int id,Feedback feedback);
        public int SubmitFeedback(Feedback feedback);
        IEnumerable<object> GetTopRatedEvents();
        object GetFeedbackSummary(int eventId);
        public List<Feedback> GetFilteredFeedbacks(
                    string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search,
                    string sortBy,
                    string sortOrder);
        public int SubmitReply(int feedbackId, Replies reply);
        public int ArchiveFeedback(int feedbackId);
        public int UnArchiveFeedback(int feedbackId);
    }
}
