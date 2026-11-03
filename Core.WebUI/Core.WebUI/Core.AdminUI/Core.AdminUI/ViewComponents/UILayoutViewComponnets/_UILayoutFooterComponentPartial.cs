using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.UILayoutViewComponnets
{
    public class _UILayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}