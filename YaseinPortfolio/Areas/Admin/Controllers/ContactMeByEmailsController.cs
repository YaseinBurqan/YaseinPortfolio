using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.Areas.Admin.Controllers
{
   [Area("Admin")]
   public class ContactMeByEmailsController : Controller
   {
      private readonly YaseinPortofolioDbContext _context;

      public ContactMeByEmailsController(YaseinPortofolioDbContext context)
      {
         _context = context;
      }

      // GET: Admin/ContactMeByEmails
      public async Task<IActionResult> Index()
      {
         return View(await _context.ContactMeByEmails.ToListAsync());
      }

      // GET: Admin/ContactMeByEmails/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var contactMeByEmail = await _context.ContactMeByEmails
             .FirstOrDefaultAsync(m => m.ContactMeByEmailId == id);
         if (contactMeByEmail == null)
         {
            return NotFound();
         }

         return View(contactMeByEmail);
      }

      // GET: Admin/ContactMeByEmails/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null)
         {
            return NotFound();
         }

         var contactMeByEmail = await _context.ContactMeByEmails
             .FirstOrDefaultAsync(m => m.ContactMeByEmailId == id);
         if (contactMeByEmail == null)
         {
            return NotFound();
         }

         return View(contactMeByEmail);
      }

      // POST: Admin/ContactMeByEmails/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var contactMeByEmail = await _context.ContactMeByEmails.FindAsync(id);
         _context.ContactMeByEmails.Remove(contactMeByEmail);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ContactMeByEmailExists(int id)
      {
         return _context.ContactMeByEmails.Any(e => e.ContactMeByEmailId == id);
      }
   }
}
