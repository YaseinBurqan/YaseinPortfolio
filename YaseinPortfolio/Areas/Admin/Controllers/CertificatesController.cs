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
    public class CertificatesController : Controller
    {
        private readonly YaseinPortofolioDbContext _context;
        private readonly IWebHostEnvironment _image;

        public CertificatesController(YaseinPortofolioDbContext context, IWebHostEnvironment image)
        {
            _context = context;
            _image = image;
        }

        // GET: Admin/Certificates
        public async Task<IActionResult> Index()
        {
            return _context.Certificates != null ?
                        View(await _context.Certificates.OrderByDescending(item => item.EntryDate).ToListAsync()) :
                        Problem("Entity set 'PortfolioDbContext.Certificates'  is null.");
        }

        // GET: Admin/Certificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(m => m.CertificateId == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // GET: Admin/Certificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Certificates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificateId,CertificateName,CertificateImg,CertificateGiver,CertificateReleaseDate,CertificatePdf,CertificateDescription")] Certificate certificate, IFormFile certificateImg)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    if (certificateImg == null || certificateImg.Length == 0)
                    {
                        return BadRequest("A valid image file is required");
                    }

                    string wwwRootPath = _image.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(certificateImg.FileName);
                    string extension = Path.GetExtension(certificateImg.FileName);

                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    string path = Path.Combine(wwwRootPath, "portfolioImages/Certificates/", fileName);

                    Directory.CreateDirectory(Path.GetDirectoryName(path));

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await certificateImg.CopyToAsync(fileStream);
                    }

                    certificate.CertificateImg = fileName;

                    _context.Add(certificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificateExists(certificate.CertificateId))
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
            return View(certificate);
        }

        // GET: Admin/Certificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate == null)
            {
                return NotFound();
            }
            return View(certificate);
        }

        // POST: Admin/Certificates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificateId,CertificateName,CertificateImg,CertificateGiver,CertificateReleaseDate,CertificatePdf,CertificateDescription")] Certificate certificate, IFormFile certificateImg)
        {
            if (id != certificate.CertificateId)
            {
                return NotFound();
            }
            var _img = certificate.CertificateImg;

            if (ModelState.IsValid)
            {
                try
                {

                    if (certificateImg == null || certificateImg.Length == 0)
                    {
                        // No new image was selected, so use the existing image
                        certificate.CertificateImg = _context.Certificates.AsNoTracking()
                           .Where(c => c.CertificateId == certificate.CertificateId)
                           .Select(c => c.CertificateImg).FirstOrDefault();
                    }
                    else
                    {
                        string wwwRootPath = _image.WebRootPath;
                        string fileName = Path.GetFileNameWithoutExtension(certificateImg.FileName);
                        string extension = Path.GetExtension(certificateImg.FileName);

                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        string path = Path.Combine(wwwRootPath, "portfolioImages/Certificates/", fileName);

                        Directory.CreateDirectory(Path.GetDirectoryName(path));

                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await certificateImg.CopyToAsync(fileStream);
                        }
                        certificate.CertificateImg = fileName;
                    }

                    _context.Update(certificate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificateExists(certificate.CertificateId))
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
            return View(certificate);
        }

        // GET: Admin/Certificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Certificates == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .FirstOrDefaultAsync(m => m.CertificateId == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Admin/Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);

            if (certificate == null)
            {
                return NotFound();
            }

            string wwwRootPath = _image.WebRootPath;
            string path = Path.Combine(wwwRootPath, "portfolioImages/Certificates/", certificate.CertificateImg);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            _context.Certificates.Remove(certificate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private bool CertificateExists(int id)
        {
            return (_context.Certificates?.Any(e => e.CertificateId == id)).GetValueOrDefault();
        }
    }
}
