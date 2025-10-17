using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class CreateFeedbackDto
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [Range(1, 5)]
        public int ContentQuality { get; set; }

        [Required]
        [Range(1, 5)]
        public int VenueFacilities { get; set; }

        [Required]
        [Range(1, 5)]
        public int EventOrganization { get; set; }

        [Required]
        [Range(1, 5)]
        public int ValueForMoney { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Comments { get; set; }
    }
}
