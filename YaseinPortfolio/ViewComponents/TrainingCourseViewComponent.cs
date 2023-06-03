using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class TrainingCourseViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public TrainingCourseViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            return View(_dbContext.TrainingCourses.OrderByDescending(item => item.EntryDate).ToList());
        }
    }
}
