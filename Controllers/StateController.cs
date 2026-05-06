using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task_Day_2_ASP.Controllers
{
    public class StateController : Controller
    {
        public IActionResult SetSession(string Name)
        {
            HttpContext.Session.SetString("name", Name);
            HttpContext.Session.SetInt32("Age",20);
            return Content("Date Saved");
        }

        public IActionResult GetAction()
        {
            string? Name = HttpContext.Session.GetString("name");
            int? Age = HttpContext.Session.GetInt32("Age");
            return Content($"Name is {Name} and Age is {Age}");
        }
    }
}
