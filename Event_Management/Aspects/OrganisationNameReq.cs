using Event_Management.Models;
using System.ComponentModel.DataAnnotations;

namespace Event_Management.Aspects
{
    public class OrganisationNameReq : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var user = (User)validationContext.ObjectInstance;

            if (user.Role == "Organiser" && string.IsNullOrWhiteSpace(user.OrganisationName))
            {
                return new ValidationResult("Organisation name is required for Organiser role.");
            }

            return ValidationResult.Success;
        }

    }
}
