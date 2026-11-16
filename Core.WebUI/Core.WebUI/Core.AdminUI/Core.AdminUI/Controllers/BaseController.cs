using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
