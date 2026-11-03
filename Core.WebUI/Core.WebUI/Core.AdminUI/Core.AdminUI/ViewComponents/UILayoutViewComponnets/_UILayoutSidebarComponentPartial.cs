using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.UILayoutViewComponnets
{
    public class _UILayoutSidebarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}