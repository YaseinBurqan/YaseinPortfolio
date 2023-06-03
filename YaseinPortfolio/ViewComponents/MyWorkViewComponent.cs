using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class MyWorkViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public MyWorkViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            return View(_dbContext.MyWorks.OrderByDescending(item => item.EntryDate).ToList());
        }
    }
}
