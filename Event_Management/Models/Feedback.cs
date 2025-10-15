using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Content Quality rating must be between 1 and 5.")]
        public int ContentQuality { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Venue Facilities rating must be between 1 and 5.")]
        public int VenueFacilities { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Event Organization rating must be between 1 and 5.")]
        public int EventOrganization { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Value for Money rating must be between 1 and 5.")]
        public int ValueForMoney { get; set; }
        [Required(ErrorMessage = "Comments cannot be empty.")]
        public string Comments { get; set; }
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
        public string? Reply { get; set; }
        public DateTime? ReplyTime { get; set; } = DateTime.Now;
        public bool IsArchived { get; set; } = false;
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
    public enum SortByOptions
    {
        SubmittedAt,
        Rating,
        FeedbackId
    }

    public enum SortOrderOptions
    {
        ascending,
        descending
    }
}
