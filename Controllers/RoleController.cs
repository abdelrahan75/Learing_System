using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task_Day_2_ASP.Models.ViewModel;

namespace Task_Day_2_ASP.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> role) {
        
            this.roleManager = role;
        }
        [HttpGet]
       public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> AddRole(RoleViewModel roleView)
        {
            if (ModelState .IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = roleView.RoleName
                };
                IdentityResult res = await roleManager.CreateAsync(role);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach(var Item in res.Errors)
                {
                    ModelState.AddModelError("",Item.Description);
                }
            }
            return View(roleView);
        }

    }
}
