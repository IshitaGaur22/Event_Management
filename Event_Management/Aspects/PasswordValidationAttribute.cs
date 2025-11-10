namespace Event_Management.Aspects
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class PasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))

                return new ValidationResult("Password is required.");

            if (password.Length < 8)
                return new ValidationResult("Password must be at least 8 characters long.");

            if (!Regex.IsMatch(password, @"[A-Za-z]") || !Regex.IsMatch(password, @"\d"))
                return new ValidationResult("Password must be alphanumeric (contain letters and numbers).");

            if (!Regex.IsMatch(password, @"[\W_]"))

                return new ValidationResult("Password must include at least one symbol.");

            return ValidationResult.Success;
        }
    }
    
}
