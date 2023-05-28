using Microsoft.AspNetCore.Mvc;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class ExperienceViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public ExperienceViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            return View(_dbContext.Experiences);
        }
    }
}
