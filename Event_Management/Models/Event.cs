

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Management.Models
{
    public class Event : IValidatableObject
    {
        [Key]
        public int EventID { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Event name must be at least 3 characters.")]
        public string EventName { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please Choose a category.")]
        public int CategoryID { get; set; }

        [Required]
        public int TotalSeats { get; set; }

        [Required]
        public decimal PricePerTicket { get; set; }

        [Required(ErrorMessage = "Please enter a valid date.")]
        [FutureOrTodayDate]
        public DateOnly EventDate { get; set; }

        [Required(ErrorMessage = "Please enter a valid time.")]
        [FutureTime]
        public TimeOnly EventTime { get; set; }

        [Required(ErrorMessage = "End Time should be taken after the start time")]
        public TimeOnly EndTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= EventTime)
            {
                yield return new ValidationResult("End time must be after start time.", new[] { nameof(EndTime) });
            }
        }

        public string? ImagePath { get; set; } = null;

    }

    public class FutureOrTodayDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateOnly date)
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                if (date < today)
                    return new ValidationResult("Date must be today or later.");
            }
            return ValidationResult.Success;
        }
    }

    public class FutureTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is not TimeOnly time)
                return ValidationResult.Success;

            var instance = validationContext.ObjectInstance;
            var type = validationContext.ObjectType;
            var dateProp = type.GetProperty("EventDate");

            if (dateProp == null || dateProp.GetValue(instance) is not DateOnly eventDate)
                return ValidationResult.Success;

            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = TimeOnly.FromDateTime(DateTime.Now);
            var nextHour = now.AddHours(1);

            // If the event is today, time must be at least one hour ahead
            if (eventDate == today && time < nextHour)
            {
                return new ValidationResult("Start time must be at least one hour from now.");
            }

            return ValidationResult.Success;
        }
    }
}
