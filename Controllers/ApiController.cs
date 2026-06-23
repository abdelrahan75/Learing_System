using Microsoft.AspNetCore.Mvc;

namespace Task_Day_2_ASP.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
