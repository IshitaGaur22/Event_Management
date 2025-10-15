using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Replies
    {
        [Key]
        public int ReplyId { get; set; }
        [Required]
        public int FeedbackId { get; set; }
        [Required]
        public string ReplyText { get; set; }
        public DateTime ReplyTime { get; set; } = DateTime.Now;
        [ForeignKey("FeedbackId")]
        public Feedback Feedback { get; set; }
    }
}
