using Microsoft.AspNetCore.Identity;

namespace Task_Day_2_ASP.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; } = string.Empty;
    }
}
