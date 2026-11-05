using Core.AdminUI.Models.AuthViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            return View();
        }

        public IActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            return View();
        }
    }
}