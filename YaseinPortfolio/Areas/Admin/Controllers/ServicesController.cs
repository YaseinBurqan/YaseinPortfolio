using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YaseinPortfolio.Models;
using YaseinPortfolio.Models.Data;
namespace YaseinPortfolio.Areas.Admin.Controllers
{
   [Area("Admin")]
   [Authorize]
   [Authorize(Roles = "Admin")]
   public class ServicesController : Controller
   {
      private readonly YaseinPortofolioDbContext _context;

      public ServicesController(YaseinPortofolioDbContext context)
      {
         _context = context;
      }

      // GET: Admin/Services
      public async Task<IActionResult> Index()
      {
         return _context.Services != null ?
                     View(await _context.Services.ToListAsync()) :
                     Problem("Entity set 'PortfolioDbContext.Services'  is null.");
      }

      // GET: Admin/Services/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || _context.Services == null)
         {
            return NotFound();
         }

         var service = await _context.Services
             .FirstOrDefaultAsync(m => m.ServiceId == id);
         if (service == null)
         {
            return NotFound();
         }

         return View(service);
      }

      // GET: Admin/Services/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Admin/Services/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("ServiceId,ServiceName,ServiceDescription")] Service service)
      {
         if (ModelState.IsValid)
         {
            _context.Add(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }
         return View(service);
      }

      // GET: Admin/Services/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.Services == null)
         {
            return NotFound();
         }

         var service = await _context.Services.FindAsync(id);
         if (service == null)
         {
            return NotFound();
         }
         return View(service);
      }

      // POST: Admin/Services/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ServiceName,ServiceDescription")] Service service)
      {
         if (id != service.ServiceId)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            try
            {
               _context.Update(service);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ServiceExists(service.ServiceId))
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
         return View(service);
      }

      // GET: Admin/Services/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || _context.Services == null)
         {
            return NotFound();
         }

         var service = await _context.Services
             .FirstOrDefaultAsync(m => m.ServiceId == id);
         if (service == null)
         {
            return NotFound();
         }

         return View(service);
      }

      // POST: Admin/Services/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         if (_context.Services == null)
         {
            return Problem("Entity set 'PortfolioDbContext.Services'  is null.");
         }
         var service = await _context.Services.FindAsync(id);
         if (service != null)
         {
            _context.Services.Remove(service);
         }

         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ServiceExists(int id)
      {
         return (_context.Services?.Any(e => e.ServiceId == id)).GetValueOrDefault();
      }

      [HttpPost]
      public async Task<IActionResult> UploadFile(IFormFile file)
      {
         if (file == null || file.Length == 0)
         {
            return Content("file not selected");
         }

         var path = Path.Combine(
                     Directory.GetCurrentDirectory(), "wwwroot",
                     file.FileName);

         using (var stream = new FileStream(path, FileMode.Create))
         {
            await file.CopyToAsync(stream);
         }

         return RedirectToAction("Index");
      }

   }
}
