using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.AuthLayoutViewComponents
{
    public class _AuthLayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}