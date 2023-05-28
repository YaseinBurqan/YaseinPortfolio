using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YaseinPortfolio.Models;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.Controllers
{
    public class ContactController : Controller
    {
        private readonly YaseinPortofolioDbContext _context;
        public ContactController(YaseinPortofolioDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendContactEmail(ContactMeByEmail model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ContactMeByEmails.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
