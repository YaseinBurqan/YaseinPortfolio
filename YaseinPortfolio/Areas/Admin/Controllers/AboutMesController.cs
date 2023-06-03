using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
    public class AboutMesController : Controller
    {
        private readonly YaseinPortofolioDbContext _context;
        private readonly IWebHostEnvironment _image;

        public AboutMesController(YaseinPortofolioDbContext context, IWebHostEnvironment image)
        {
            _context = context;
            _image = image;
        }

        // GET: Admin/AboutMes
        public async Task<IActionResult> Index()
        {
            return _context.AboutMes != null ?
                        View(await _context.AboutMes.OrderByDescending(item => item.AboutMeEntryDate).ToListAsync()) :
                        Problem("Entity set 'PortfolioDbContext.AboutMes'  is null.");
        }

        // GET: Admin/AboutMes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AboutMes == null)
            {
                return NotFound();
            }

            var aboutMe = await _context.AboutMes
                .FirstOrDefaultAsync(m => m.AboutMeId == id);
            if (aboutMe == null)
            {
                return NotFound();
            }

            return View(aboutMe);
        }

        // GET: Admin/AboutMes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AboutMes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("AboutMeId,AboutMeMyName,AboutMeSubtitle,AboutMeParagraph")] AboutMe aboutMe, IFormFile aboutMeImage)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutMeImage == null || aboutMeImage.Length == 0)
                    {
                        return BadRequest("A valid image file is required");
                    }

                    string wwwRootPath = _image.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(aboutMeImage.FileName);
                    string extension = Path.GetExtension(aboutMeImage.FileName);

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "portfolioImages/AboutMe/", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await aboutMeImage.CopyToAsync(fileStream);
                    }

                    aboutMe.AboutMeImage = fileName;

                    _context.Add(aboutMe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutMeExists(aboutMe.AboutMeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(aboutMe);
        }

        // GET: Admin/AboutMes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AboutMes == null)
            {
                return NotFound();
            }

            var aboutMe = await _context.AboutMes.FindAsync(id);
            if (aboutMe == null)
            {
                return NotFound();
            }
            return View(aboutMe);
        }

        // POST: Admin/AboutMes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AboutMeId,AboutMeMyName,AboutMeImage,AboutMeSubtitle,AboutMeParagraph")] AboutMe aboutMe, IFormFile aboutMeImage)
        {
            if (id != aboutMe.AboutMeId)
            {
                return NotFound();
            }
            var _img = aboutMe.AboutMeImage;

            if (ModelState.IsValid)
            {
                try
                {

                    if (aboutMeImage == null || aboutMeImage.Length == 0)
                    {
                        // No new image was selected, so use the existing image
                        aboutMe.AboutMeImage = _context.AboutMes.AsNoTracking()
                           .Where(c => c.AboutMeId == aboutMe.AboutMeId)
                           .Select(c => c.AboutMeImage)
                           .FirstOrDefault();
                    }
                    else
                    {
                        string wwwRootPath = _image.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(aboutMeImage.FileName);
                        string extension = Path.GetExtension(aboutMeImage.FileName);

                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath, "portfolioImages/AboutMe/", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await aboutMeImage.CopyToAsync(fileStream);
                        }

                        aboutMe.AboutMeImage = fileName;
                    }

                    _context.Update(aboutMe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutMeExists(aboutMe.AboutMeId))
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
            return View(aboutMe);
        }

        // GET: Admin/AboutMe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AboutMes == null)
            {
                return NotFound();
            }

            var aboutMe = await _context.AboutMes
                .FirstOrDefaultAsync(m => m.AboutMeId == id);
            if (aboutMe == null)
            {
                return NotFound();
            }

            return View(aboutMe);
        }

        // POST: Admin/AboutMe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aboutMe = await _context.AboutMes.FindAsync(id);

            if (aboutMe == null)
            {
                return NotFound();
            }

            string wwwRootPath = _image.WebRootPath;
            string path = Path.Combine(wwwRootPath, "portfolioImages/AboutMe/", aboutMe.AboutMeImage);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.AboutMes.Remove(aboutMe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool AboutMeExists(int id)
        {
            return (_context.AboutMes?.Any(e => e.AboutMeId == id)).GetValueOrDefault();
        }
    }
}
