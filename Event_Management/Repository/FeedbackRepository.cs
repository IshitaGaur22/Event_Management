using Event_Management.Data;
//using Event_Management.Migrations;
using Event_Management.Models;
//using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    //repository folder is for logical part using c#
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly Event_ManagementContext _context;
        public FeedbackRepository(Event_ManagementContext context)
        {
            _context = context;
        }
        
        public List<Feedback> GetAllFeedbacks()
        {

               return _context.Feedback.Include(f => f.Event).Where(f=>f.IsArchived==false).ToList();
            
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedback.Where(f => f.IsArchived == false).FirstOrDefault(s => s.FeedbackId == id);
        }
        public Feedback FindFeedbackById_Admin(int id)
        {
            return _context.Feedback.FirstOrDefault(s => s.FeedbackId == id);
        }

        public int UpdateFeedback(int id, Feedback feedback)
        {
            var existingFeedback = _context.Feedback.FirstOrDefault(s => s.FeedbackId == id);

            if (existingFeedback != null)
            {
                existingFeedback.Rating = feedback.Rating;
                existingFeedback.Comments = feedback.Comments;
            }
            return _context.SaveChanges();

        }

        public bool HasUserAttendedEvent(int userId, int eventId)
        {
            return _context.Booking
                .Any(b => b.UserId == userId && b.EventId == eventId);
        }
        public bool HasUserAlreadySubmittedFeedback(int userId, int eventId)
        {
            return _context.Feedback.Any(f => f.UserId == userId && f.EventId == eventId);
        }
        public int SubmitFeedback(Feedback feedback)
        {
            
            var feed = new Feedback
            {
                UserId = feedback.UserId,
                EventId = feedback.EventId,
                Rating = feedback.Rating,
                ContentQuality=feedback.ContentQuality,
                VenueFacilities=feedback.VenueFacilities,
                EventOrganization=feedback.EventOrganization,
                ValueForMoney=feedback.ValueForMoney,
                Comments = feedback.Comments,
                SubmittedAt = DateTime.Now,
                IsArchived=feedback.IsArchived,
                Reply = null,
                ReplyTime = null
            };

            _context.Feedback.Add(feed);
            return _context.SaveChanges();
            Console.WriteLine("Your feedback has been submitted successfully.");

        }

        public IEnumerable<object> GetTopRatedEvents()
        {
            var topRatedEvents = _context.Feedback
                .Include(f => f.Event)
                .Where(f => !f.IsArchived)
                .GroupBy(f => new { f.EventId, f.Event.EventName })
                .Where(g => g.Count() >= 2)
                .OrderByDescending(g => g.Average(f => f.Rating))
                .Select(g => new
                {
                    EventName = g.Key.EventName,
                    AverageRating = g.Average(f => f.Rating),
                    FeedbackCount = g.Count()
                }).ToList();
            return topRatedEvents;
        }

        public object GetFeedbackSummary(int eventId)
        {
            var summary = _context.Feedback
                .Where(f => f.EventId == eventId && !f.IsArchived)
                .GroupBy(f => f.EventId)
                .Select(g => new
                {
                    TotalFeedback = g.Count(),
                    AverageRating = g.Average(f => f.Rating)
                })
                .FirstOrDefault();
            return summary ?? new { TotalFeedback = 0, AverageRating = 0.0 };
        }
        public List<Feedback> GetFilteredFeedbacks(
                    string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search)
        {
            var query = _context.Feedback.Include(f => f.Event).AsQueryable();

            if (!string.IsNullOrWhiteSpace(eventName))
                query = query.Where(f => f.Event != null && f.Event.EventName == eventName);

            if (minRating.HasValue)
                query = query.Where(f => f.Rating >= minRating.Value);

            if (startDate.HasValue)
                query = query.Where(f => f.SubmittedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(f => f.SubmittedAt <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(f => f.Comments.Contains(search));

            return query.ToList();
        }

        public Replies GetReplyById(int id)
        {
            return _context.Replies.FirstOrDefault(s => s.FeedbackId == id);
        }

        public int AddReply(int feedbackId,Replies replies)
        {
            var rep = new Replies
            {
                FeedbackId = feedbackId,
                ReplyText = replies.ReplyText,
                ReplyTime = DateTime.Now
            };
            _context.Replies.Add(rep);
            var feedback = _context.Feedback.FirstOrDefault(f => f.FeedbackId == feedbackId);
            
            feedback.Reply = replies.ReplyText;
            feedback.ReplyTime = replies.ReplyTime;
            return _context.SaveChanges();
            
        }
        public int ArchiveFeedback(int feedbackId)
        {
            var feed=_context.Feedback.FirstOrDefault(s => s.FeedbackId == feedbackId);
            feed.IsArchived = true;
            return _context.SaveChanges();
        }
        public int UnArchiveFeedback(int feedbackId)
        {
            var feed = _context.Feedback.FirstOrDefault(s => s.FeedbackId == feedbackId);
            feed.IsArchived = false;
            return _context.SaveChanges();
        }

    }
}
