using Microsoft.AspNetCore.Mvc;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
   public class ServiceViewComponent : ViewComponent
   {
      private readonly YaseinPortofolioDbContext _dbContext;
      public ServiceViewComponent(YaseinPortofolioDbContext dbContext)
      {
         _dbContext = dbContext;
      }
      public IViewComponentResult Invoke()
      {
         return View(_dbContext.Services);
      }
   }
}
