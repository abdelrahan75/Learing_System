using System.ComponentModel.DataAnnotations;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.ValidationModels.StudentValidation
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            string? Validname = value.ToString();

            LearningDbContext? context = validationContext.GetService(typeof(LearningDbContext)) as LearningDbContext;
            Student? newstd = context.Students.FirstOrDefault(S => S.Name == Validname);

            if (newstd == null)
            {
                return ValidationResult.Success;
            }
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
