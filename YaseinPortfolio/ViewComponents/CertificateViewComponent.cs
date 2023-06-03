using Microsoft.AspNetCore.Mvc;
using System.Linq;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.ViewComponents
{
    public class CertificateViewComponent : ViewComponent
    {
        private readonly YaseinPortofolioDbContext _dbContext;
        public CertificateViewComponent(YaseinPortofolioDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke()
        {
            return View(_dbContext.Certificates.OrderByDescending(item => item.EntryDate).ToList());
        }
    }
}
