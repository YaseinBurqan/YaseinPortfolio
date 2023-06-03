using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class SkillViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public SkillViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            var result = _dbContext.Skills.OrderByDescending(item => item.EntryDate).ToList();
            return View(result);
        }
    }
}
