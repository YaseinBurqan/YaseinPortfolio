using Microsoft.AspNetCore.Mvc;

namespace YaseinPortfolio.ViewComponents
{
    public class ContactSectionViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
