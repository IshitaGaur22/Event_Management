using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Booking ID")]
        public int BookingId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey(nameof(User))]
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Event ID is required.")]
        [ForeignKey(nameof(Event))]
        [Display(Name = "Event ID")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Selected Seats count is required.")]
        [Range(1, 1000, ErrorMessage = "Selected Seats must be between 1 and 1000.")]
        [Display(Name = "Seats Booked")]
        public int SelectedSeats { get; set; }

        [Required(ErrorMessage = "Booking Date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = "Upcoming"; 
        //public string Description { get; set; }

        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
}