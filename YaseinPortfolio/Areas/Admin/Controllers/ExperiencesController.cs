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
   public class ExperiencesController : Controller
   {

      private readonly YaseinPortofolioDbContext _context;
      private readonly IWebHostEnvironment _image;

      public ExperiencesController(YaseinPortofolioDbContext context, IWebHostEnvironment image)
      {
         _context = context;
         _image = image;
      }

      // GET: Admin/Experiences
      public async Task<IActionResult> Index()
      {
         return _context.Experiences != null ?
                     View(await _context.Experiences.ToListAsync()) :
                     Problem("Entity set 'PortfolioDbContext.Experiences'  is null.");
      }

      // GET: Admin/Experiences/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || _context.Experiences == null)
         {
            return NotFound();
         }

         var experience = await _context.Experiences
             .FirstOrDefaultAsync(m => m.ExperienceId == id);
         if (experience == null)
         {
            return NotFound();
         }

         return View(experience);
      }

      // GET: Admin/Experiences/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Admin/Experiences/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("ExperienceId, ExperienceJobName, ExperienceCompanyName, ExperienceCompanyImg, ExperienceStartDate, ExperienceEndtDate, ExperienceCertificatePdf, ExperienceDescription")] Experience experience, IFormFile ExperienceCompanyImg)
      {
         if (!ModelState.IsValid)
         {
            return View(experience);
         }

         try
         {
            if (ExperienceCompanyImg == null || ExperienceCompanyImg.Length == 0)
            {
               return BadRequest("A valid image file is required");
            }

            string wwwRootPath = _image.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(ExperienceCompanyImg.FileName);
            string extension = Path.GetExtension(ExperienceCompanyImg.FileName);

            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(wwwRootPath, "portfolioImages", "Experiences", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
               await ExperienceCompanyImg.CopyToAsync(fileStream);
            }

            experience.ExperienceCompanyImg = fileName;

            _context.Add(experience);
            await _context.SaveChangesAsync();
         }
         catch (DbUpdateConcurrencyException)
         {
            if (!ExperienceExists(experience.ExperienceId))
            {
               return NotFound();
            }
            else
            {
               throw;
            }
         }
         catch (Exception ex)
         {
            return BadRequest($"An error occurred while saving the image: {ex.Message}");
         }

         return RedirectToAction(nameof(Index));
      }


      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.Experiences == null)
         {
            return NotFound();
         }

         var experiences = await _context.Experiences
             .FirstOrDefaultAsync(m => m.ExperienceId == id);
         if (experiences == null)
         {
            return NotFound();
         }
         return View(experiences);
      }

      // POST: Admin/Experiences/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("ExperienceId,ExperienceJobName,ExperienceCompanyName,ExperienceCompanyImg,ExperienceStartDate,ExperienceEndtDate,ExperienceDescription,ExperienceCertificatePdf")] Experience experience, IFormFile ExperienceCompanyImg)
      {
         if (id != experience.ExperienceId)
         {
            return NotFound();
         }
         var _img = experience.ExperienceCompanyImg;

         if (ModelState.IsValid)
         {
            try
            {

               if (ExperienceCompanyImg == null || ExperienceCompanyImg.Length == 0)
               {
                  // No new image was selected, so use the existing image
                  experience.ExperienceCompanyImg = _context.Experiences.AsNoTracking()
                     .Where(c => c.ExperienceId == experience.ExperienceId)
                     .Select(c => c.ExperienceCompanyImg).FirstOrDefault();
               }
               else
               {
                  string wwwRootPath = _image.WebRootPath;
                  string fileName = Path.GetFileNameWithoutExtension(ExperienceCompanyImg.FileName);
                  string extension = Path.GetExtension(ExperienceCompanyImg.FileName);

                  fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                  string path = Path.Combine(wwwRootPath, "portfolioImages/Experiences/", fileName);

                  Directory.CreateDirectory(Path.GetDirectoryName(path));

                  using (var fileStream = new FileStream(path, FileMode.Create))
                  {
                     await ExperienceCompanyImg.CopyToAsync(fileStream);
                  }
                  experience.ExperienceCompanyImg = fileName;
               }


               _context.Update(experience);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!ExperienceExists(experience.ExperienceId))
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
         return View(experience);
      }

      // GET: Admin/Experiences/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || _context.Experiences == null)
         {
            return NotFound();
         }

         var experience = await _context.Experiences
             .FirstOrDefaultAsync(m => m.ExperienceId == id);
         if (experience == null)
         {
            return NotFound();
         }

         return View(experience);
      }

      // POST: Admin/Experiences/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var experiences = await _context.Experiences.FindAsync(id);

         if (experiences == null)
         {
            return NotFound();
         }

         string wwwRootPath = _image.WebRootPath;
         string path = Path.Combine(wwwRootPath, "portfolioImages/Experiences/", experiences.ExperienceCompanyImg);

         if (System.IO.File.Exists(path))
         {
            System.IO.File.Delete(path);
         }

         _context.Experiences.Remove(experiences);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool ExperienceExists(int id)
      {
         return (_context.Experiences?.Any(e => e.ExperienceId == id)).GetValueOrDefault();
      }
   }
}
