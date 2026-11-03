using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.UILayoutViewComponnets
{
    public class _UILayoutNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}