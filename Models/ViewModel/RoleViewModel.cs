using Microsoft.AspNetCore.SignalR.Protocol;
using System.ComponentModel.DataAnnotations;

namespace Task_Day_2_ASP.Models.ViewModel
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "*")]
        public string RoleName { get; set; }
    }
}
