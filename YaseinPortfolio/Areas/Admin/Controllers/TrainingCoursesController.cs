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
    public class TrainingCoursesController : Controller
    {
        private readonly YaseinPortofolioDbContext _context;

        public TrainingCoursesController(YaseinPortofolioDbContext context)
        {
            _context = context;
        }

        // GET: Admin/TrainingCourses
        public async Task<IActionResult> Index()
        {
            return _context.TrainingCourses != null ?
                        View(await _context.TrainingCourses.OrderByDescending(item => item.EntryDate).ToListAsync()) :
                        Problem("Entity set 'PortfolioDbContext.TrainingCourses'  is null.");
        }

        // GET: Admin/TrainingCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TrainingCourses == null)
            {
                return NotFound();
            }

            var trainingCourse = await _context.TrainingCourses
                .FirstOrDefaultAsync(m => m.TrainingCourseId == id);
            if (trainingCourse == null)
            {
                return NotFound();
            }

            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/TrainingCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrainingCourseId,TrainingCourseName,TrainingCourseImg,CertificateBodyIssuingTheCertificate,TrainingCoursePdfUrl")] TrainingCourse trainingCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TrainingCourses == null)
            {
                return NotFound();
            }

            var trainingCourse = await _context.TrainingCourses.FindAsync(id);
            if (trainingCourse == null)
            {
                return NotFound();
            }
            return View(trainingCourse);
        }

        // POST: Admin/TrainingCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TrainingCourseId,TrainingCourseName,TrainingCourseImg,CertificateBodyIssuingTheCertificate,TrainingCoursePdfUrl")] TrainingCourse trainingCourse)
        {
            if (id != trainingCourse.TrainingCourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingCourseExists(trainingCourse.TrainingCourseId))
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
            return View(trainingCourse);
        }

        // GET: Admin/TrainingCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TrainingCourses == null)
            {
                return NotFound();
            }

            var trainingCourse = await _context.TrainingCourses
                .FirstOrDefaultAsync(m => m.TrainingCourseId == id);
            if (trainingCourse == null)
            {
                return NotFound();
            }

            return View(trainingCourse);
        }

        // POST: Admin/TrainingCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TrainingCourses == null)
            {
                return Problem("Entity set 'PortfolioDbContext.TrainingCourses'  is null.");
            }
            var trainingCourse = await _context.TrainingCourses.FindAsync(id);
            if (trainingCourse != null)
            {
                _context.TrainingCourses.Remove(trainingCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingCourseExists(int id)
        {
            return (_context.TrainingCourses?.Any(e => e.TrainingCourseId == id)).GetValueOrDefault();
        }
    }
}
