using Microsoft.AspNetCore.Mvc;
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
         return View(_dbContext.Skills);
      }
   }
}
