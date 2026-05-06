using System.ComponentModel.DataAnnotations;

namespace Task_Day_2_ASP.ValidationModels.StudentValidation
{
    public class NotDefaultAgeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is int age && age <= 0)
            {
                return new ValidationResult(ErrorMessage ?? "Age must be a positive number.");
            }

            return ValidationResult.Success;
        }
    }
}
