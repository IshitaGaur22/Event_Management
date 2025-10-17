using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class ReplyDto
    {
        [Required]
        public string ReplyText { get; set; }
        public DateTime ReplyTime { get; set; } = DateTime.Now;
    }
}
