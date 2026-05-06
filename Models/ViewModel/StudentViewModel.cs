using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.ViewModel
{
    public class StudentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(5, 100)]
        public int Age { get; set; }

        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

    }
}
