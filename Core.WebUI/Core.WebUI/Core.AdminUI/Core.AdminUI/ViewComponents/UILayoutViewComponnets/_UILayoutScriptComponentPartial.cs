using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.UILayoutViewComponnets
{
    public class _UILayoutScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}