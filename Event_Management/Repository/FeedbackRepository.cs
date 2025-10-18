using Event_Management.Data;
using Event_Management.DTOs;
using Event_Management.Migrations;
using Event_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly Event_ManagementContext _context;
        public FeedbackRepository(Event_ManagementContext context)
        {
            _context = context;
        }
        
        public List<Feedback> GetAllFeedbacks()
        {
               return _context.Feedback.Where(f=>f.IsArchived==false).ToList();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedback.Where(f => f.IsArchived == false).FirstOrDefault(s => s.FeedbackId == id);
        }
        public Feedback FindFeedbackById_Admin(int id)
        {
            return _context.Feedback.FirstOrDefault(s => s.FeedbackId == id);
        }

        public bool HasUserAttendedEvent(int userId, int eventId)
        {
            return _context.Booking
                .Any(b => b.UserId == userId && b.EventId == eventId && b.Status=="Attended");
        }
        public bool HasUserAlreadySubmittedFeedback(int userId, int eventId)
        {
            return _context.Feedback.Any(f => f.UserId == userId && f.EventId == eventId);
        }
        public int SubmitFeedback(CreateFeedbackDto feedbackDto)
        {

            var feedback = new Feedback
            {
                EventId = feedbackDto.EventId,
                UserId = feedbackDto.UserId,
                Rating = feedbackDto.Rating,
                ContentQuality = feedbackDto.ContentQuality,
                VenueFacilities = feedbackDto.VenueFacilities,
                EventOrganization = feedbackDto.EventOrganization,
                ValueForMoney = feedbackDto.ValueForMoney,
                Comments = feedbackDto.Comments,
                SubmittedAt = DateTime.UtcNow 
            };

            _context.Feedback.Add(feedback);
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
        public IEnumerable<object> GetFilteredFeedbacks(
                    string? eventName,
                    int? minRating,
                    DateTime? startDate,
                    DateTime? endDate,
                    string? search)
        {
            var query = _context.Feedback.Include(f => f.Event).Include(u=>u.User).AsQueryable();

            if (!string.IsNullOrWhiteSpace(eventName))
                query = query.Where(f => f.Event != null && f.Event.EventName.Contains(eventName));

            if (minRating.HasValue)
                query = query.Where(f => f.Rating >= minRating.Value);

            if (startDate.HasValue)
                query = query.Where(f => f.SubmittedAt >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(f => f.SubmittedAt <= endDate.Value);

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(f => f.Comments.Contains(search));

            var result = query.Select(f => new
            {
                FeedbackId = f.FeedbackId,
                EventName = f.Event.EventName,
                UserName = f.User.UserName,
                Rating = f.Rating,
                ContentQuality=f.ContentQuality,
                VenueFacilities=f.VenueFacilities,
                EventOrganization=f.EventOrganization,
                ValueForMoney=f.ValueForMoney,
                Comments = f.Comments,
                SubmittedAt = f.SubmittedAt,
                Reply = f.Reply,
                ReplyTime = f.ReplyTime
            });
            return result;
        }

        public Replies GetReplyById(int id)
        {
            return _context.Replies.FirstOrDefault(s => s.FeedbackId == id);
        }

        public int AddReply(int feedbackId, ReplyDto replies)
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
            return _context.Database.ExecuteSqlRaw("EXEC ArchiveFeedbackById @FeedbackId = {0}",feedbackId);
        }
        public int UnArchiveFeedback(int feedbackId)
        {
            var feed = _context.Feedback.FirstOrDefault(s => s.FeedbackId == feedbackId);
            feed.IsArchived = false;
            return _context.SaveChanges();
        }

    }
}
