using Microsoft.AspNetCore.Mvc;

namespace Core.AdminUI.ViewComponents.UILayoutViewComponnets
{
    public class _UILayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}