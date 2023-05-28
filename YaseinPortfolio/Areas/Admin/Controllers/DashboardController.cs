using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YaseinPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
