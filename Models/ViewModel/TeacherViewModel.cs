using System.ComponentModel.DataAnnotations;

namespace Task_Day_2_ASP.Models.ViewModel
{
    public class TeacherViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Teacher name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, 999999.99, ErrorMessage = "Salary must be a positive value.")]
        public decimal Salary { get; set; }

        public int? CourseId { get; set; }
        public int? DepartmentId { get; set; }

        public string? CourseName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
