using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.ViewModel;

namespace Task_Day_2_ASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ViewModelRegister viewModelRegister)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = viewModelRegister.UserName,
                    Address = viewModelRegister.Address ?? string.Empty
                };
                IdentityResult res = await _userManager.CreateAsync(user, viewModelRegister.Password);
                if (res.Succeeded)
                {
                    _userManager.AddToRoleAsync(user,"Admin")
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Students");
                }
                foreach (var item in res.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(viewModelRegister);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(ViewModelLogin viewModelLogin)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? userApp = await _userManager.FindByNameAsync(viewModelLogin.UserName);
                if (userApp != null)
                {
                    bool isValidPassword = await _userManager.CheckPasswordAsync(userApp, viewModelLogin.Password);
                    if (isValidPassword)
                    {
                        await _signInManager.SignInAsync(userApp, false);
                        return RedirectToAction("Index", "Students");
                    }
                }
                ModelState.AddModelError("", "Invalid username or password");
            }

            return RedirectToAction("Index", viewModelLogin);
        }
    }
}
