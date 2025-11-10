using System.ComponentModel.DataAnnotations;

namespace Event_Management.DTOs
{
    public class UpdateBookingDto
    {
        [Required(ErrorMessage = "SelectedSeats is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "SelectedSeats must be a positive number.")]
        public int SelectedSeats { get; set; }
    }
}