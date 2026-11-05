using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.AuthLayoutViewComponents
{
    public class _AuthLayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}