using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.AuthLayoutViewComponents
{
    public class _AuthLayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}