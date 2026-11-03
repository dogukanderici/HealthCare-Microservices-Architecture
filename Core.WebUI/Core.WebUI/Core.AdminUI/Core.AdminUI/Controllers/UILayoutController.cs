using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}