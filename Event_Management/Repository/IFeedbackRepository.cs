using Event_Management.DTOs;
using Event_Management.Models;

namespace Event_Management.Repository
{
    public interface IFeedbackRepository
    {

        List<Feedback> GetAllFeedbacks();
        Feedback GetFeedbackById(int id);
        Feedback FindFeedbackById_Admin(int id);
        int SubmitFeedback(CreateFeedbackDto feedbackDto);
        bool HasUserAttendedEvent(int userId, int eventId);
        bool HasUserAlreadySubmittedFeedback(int userId, int eventId);
        IEnumerable<object> GetTopRatedEvents();
        object GetFeedbackSummary(int eventId);
        IEnumerable<object> GetFilteredFeedbacks(string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search);
        Replies GetReplyById(int id);
        int AddReply(int feedbckId, ReplyDto reply);
        int ArchiveFeedback(int feedbackId);
        int UnArchiveFeedback(int feedbackId);
    }
}
