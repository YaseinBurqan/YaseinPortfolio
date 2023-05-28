using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class AboutMeViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public AboutMeViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            var LatestAboutMe = _dbContext.AboutMes.OrderByDescending(item => item.AboutMeEntryDate).Take(1).ToList();

            return View(LatestAboutMe);
        }
    }
}
