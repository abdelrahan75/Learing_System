using System.ComponentModel.DataAnnotations;

namespace Task_Day_2_ASP.Models.ViewModel
{
    public class ViewModelLogin
    {
        [Required(ErrorMessage = "*")]
        [StringLength(100)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
