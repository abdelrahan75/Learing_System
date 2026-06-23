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

        public IActionResult Demo()
        {
            var visitCount = HttpContext.Session.GetInt32("VisitCount") ?? 0;
            visitCount++;
            HttpContext.Session.SetInt32("VisitCount", visitCount);

            return View();
        }

        [HttpPost]
        public IActionResult SetWelcome(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                HttpContext.Session.SetString("WelcomeUser", userName);
            }
            return RedirectToAction(nameof(Demo));
        }

        public IActionResult ClearSession()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Demo));
        }
    }
}
