using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Rasttad.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        // خروج از حساب
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // صفحه 404
        public IActionResult Error404()
        {
            return View("NotFound");
        }
    }
}