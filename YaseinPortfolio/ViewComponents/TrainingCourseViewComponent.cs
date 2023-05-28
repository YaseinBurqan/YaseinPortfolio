using Microsoft.AspNetCore.Mvc;
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
            return View(_dbContext.TrainingCourses);
        }
    }
}
