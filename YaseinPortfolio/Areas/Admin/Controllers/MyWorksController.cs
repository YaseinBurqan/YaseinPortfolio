using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YaseinPortfolio.Models;
using YaseinPortfolio.Models.Data;

namespace YaseinPortfolio.Areas.Admin.Controllers
{
   [Area("Admin")]
   [Authorize]
   [Authorize(Roles = "Admin")]
   public class MyWorksController : Controller
   {
      private readonly YaseinPortofolioDbContext _context;

      public MyWorksController(YaseinPortofolioDbContext context)
      {
         _context = context;
      }

      // GET: Admin/MyWorks
      public async Task<IActionResult> Index()
      {
         return _context.MyWorks != null ?
                     View(await _context.MyWorks.ToListAsync()) :
                     Problem("Entity set 'PortfolioDbContext.MyWorks'  is null.");
      }

      // GET: Admin/MyWorks/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || _context.MyWorks == null)
         {
            return NotFound();
         }

         var myWork = await _context.MyWorks
             .FirstOrDefaultAsync(m => m.MyWorkId == id);
         if (myWork == null)
         {
            return NotFound();
         }

         return View(myWork);
      }

      // GET: Admin/MyWorks/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Admin/MyWorks/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("MyWorkId,MyWorkName,MyWorkImg,MyWorkUrl")] MyWork myWork)
      {
         if (ModelState.IsValid)
         {
            _context.Add(myWork);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(myWork);
      }

      // GET: Admin/MyWorks/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.MyWorks == null)
         {
            return NotFound();
         }

         var myWork = await _context.MyWorks.FindAsync(id);
         if (myWork == null)
         {
            return NotFound();
         }
         return View(myWork);
      }

      // POST: Admin/MyWorks/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("MyWorkId,MyWorkName,MyWorkImg,MyWorkUrl")] MyWork myWork)
      {
         if (id != myWork.MyWorkId)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(myWork);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!MyWorkExists(myWork.MyWorkId))
               {
                  return NotFound();
               }
               else
               {
                  throw;
               }
            }
            return RedirectToAction(nameof(Index));
         }
         return View(myWork);
      }

      // GET: Admin/MyWorks/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || _context.MyWorks == null)
         {
            return NotFound();
         }

         var myWork = await _context.MyWorks
             .FirstOrDefaultAsync(m => m.MyWorkId == id);
         if (myWork == null)
         {
            return NotFound();
         }

         return View(myWork);
      }

      // POST: Admin/MyWorks/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         if (_context.MyWorks == null)
         {
            return Problem("Entity set 'PortfolioDbContext.MyWorks'  is null.");
         }
         var myWork = await _context.MyWorks.FindAsync(id);
         if (myWork != null)
         {
            _context.MyWorks.Remove(myWork);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool MyWorkExists(int id)
      {
         return (_context.MyWorks?.Any(e => e.MyWorkId == id)).GetValueOrDefault();
      }
   }
}
