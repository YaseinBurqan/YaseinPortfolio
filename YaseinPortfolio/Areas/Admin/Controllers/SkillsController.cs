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
   public class SkillsController : Controller
   {
      private readonly YaseinPortofolioDbContext _context;
      private readonly IWebHostEnvironment _image;

      public SkillsController(YaseinPortofolioDbContext context, IWebHostEnvironment image)
      {
         _context = context;
         _image = image;
      }

      // GET: Admin/Skills
      public async Task<IActionResult> Index()
      {
         return _context.Skills != null ?
                     View(await _context.Skills.ToListAsync()) :
                     Problem("Entity set 'PortfolioDbContext.Skills'  is null.");
      }

      // GET: Admin/Skills/Details/5
      public async Task<IActionResult> Details(int? id)
      {
         if (id == null || _context.Skills == null)
         {
            return NotFound();
         }

         var skill = await _context.Skills
             .FirstOrDefaultAsync(m => m.SkillId == id);
         if (skill == null)
         {
            return NotFound();
         }

         return View(skill);
      }

      // GET: Admin/Skills/Create
      public IActionResult Create()
      {
         return View();
      }

      // POST: Admin/Skills/Create
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind("SkillId,SkillName,SkillImg")] Skill skill, IFormFile SkillImg)
      {

         if (ModelState.IsValid)
         {
            try
            {

               if (SkillImg == null || SkillImg.Length == 0)
               {
                  return BadRequest("A valid image file is required");
               }

               string wwwRootPath = _image.WebRootPath;
               string fileName = Path.GetFileNameWithoutExtension(SkillImg.FileName);
               string extension = Path.GetExtension(SkillImg.FileName);

               fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
               string path = Path.Combine(wwwRootPath, "portfolioImages/Skills/", fileName);

               Directory.CreateDirectory(Path.GetDirectoryName(path));

               using (var fileStream = new FileStream(path, FileMode.Create))
               {
                  await SkillImg.CopyToAsync(fileStream);
               }

               skill.SkillImg = fileName;


               _context.Add(skill);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!SkillExists(skill.SkillId))
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
         return View(skill);
      }

      // GET: Admin/Skills/Edit/5
      public async Task<IActionResult> Edit(int? id)
      {
         if (id == null || _context.Skills == null)
         {
            return NotFound();
         }

         var skill = await _context.Skills
             .FirstOrDefaultAsync(m => m.SkillId == id);
         if (skill == null)
         {
            return NotFound();
         }
         return View(skill);
      }

      // POST: Admin/Skills/Edit/5
      // To protect from overposting attacks, enable the specific properties you want to bind to.
      // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Edit(int id, [Bind("SkillId,SkillName,SkillImg")] Skill skill, IFormFile SkillImg)
      {
         if (id != skill.SkillId)
         {
            return NotFound();
         }

         if (ModelState.IsValid)
         {
            if (SkillImg == null || SkillImg.Length == 0)
            {
               // No new image was selected, so keep the existing image
               skill.SkillImg = _context.Skills
                  .AsNoTracking()
                  .Where(c => c.SkillId == skill.SkillId)
                  .Select(c => c.SkillImg)
                  .FirstOrDefault();
            }
            try
            {

               if (SkillImg == null || SkillImg.Length == 0)
               {
                  return BadRequest("A valid image file is required");
               }

               string wwwRootPath = _image.WebRootPath;
               string fileName = Path.GetFileNameWithoutExtension(SkillImg.FileName);
               string extension = Path.GetExtension(SkillImg.FileName);

               fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
               string path = Path.Combine(wwwRootPath, "portfolioImages/Skills/", fileName);

               Directory.CreateDirectory(Path.GetDirectoryName(path));

               using (var fileStream = new FileStream(path, FileMode.Create))
               {
                  await SkillImg.CopyToAsync(fileStream);
               }

               skill.SkillImg = fileName;


               _context.Update(skill);
               await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               if (!SkillExists(skill.SkillId))
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
         return View(skill);
      }

      // GET: Admin/Certificates/Delete/5
      public async Task<IActionResult> Delete(int? id)
      {
         if (id == null || _context.Skills == null)
         {
            return NotFound();
         }

         var skill = await _context.Skills
             .FirstOrDefaultAsync(m => m.SkillId == id);
         if (skill == null)
         {
            return NotFound();
         }

         return View(skill);
      }


      // POST: Admin/Skills/Delete/5
      [HttpPost, ActionName("Delete")]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> DeleteConfirmed(int id)
      {
         var skill = await _context.Skills.FindAsync(id);

         if (skill == null)
         {
            return NotFound();
         }

         string wwwRootPath = _image.WebRootPath;
         string path = Path.Combine(wwwRootPath, "portfolioImages/Skills/", skill.SkillImg);

         if (System.IO.File.Exists(path))
         {
            System.IO.File.Delete(path);
         }

         _context.Skills.Remove(skill);
         await _context.SaveChangesAsync();
         return RedirectToAction(nameof(Index));
      }

      private bool SkillExists(int id)
      {
         return (_context.Skills?.Any(e => e.SkillId == id)).GetValueOrDefault();
      }
   }
}
