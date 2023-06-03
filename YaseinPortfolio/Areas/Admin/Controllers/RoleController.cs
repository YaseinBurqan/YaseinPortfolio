using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YaseinPortfolio.Models.ViewModels;

namespace YaseinPortfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        #region InjectionAndObjects
        private RoleManager<IdentityRole> _context;

        public RoleController(RoleManager<IdentityRole> context)
        {
            _context = context;
        }
        #endregion

        // GET: Admin/Role
        public async Task<IActionResult> Index()
        {
            return _context.Roles != null ?
                        View(await _context.Roles.ToListAsync()) :
                        Problem("Entity set 'PortfolioDbContext.RoleViewModel'  is null.");
        }

        // GET: Admin/Role/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }
            var roleViewModel = await _context.FindByIdAsync(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }
            RoleViewModel model = new RoleViewModel
            {
                RoleViewModelId = roleViewModel.Id,
                RoleName = roleViewModel.Name
            };
            return View(model);
        }

        // GET: Admin/Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateRoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = roleViewModel.RoleName
                };

                var result = await _context.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(roleViewModel);
        }

        [HttpGet]
        // GET: Admin/Role/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var roleViewModel = await _context.FindByIdAsync(id);

            if (roleViewModel == null)
            {
                return NotFound();
            }

            EditRoleViewModel model = new EditRoleViewModel
            {
                RoleId = roleViewModel.Id,
                RoleName = roleViewModel.Name
            };

            return View(model);
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _context.FindByIdAsync(model.RoleId);
                role.Name = model.RoleName;
                var result = await _context.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        // GET: Admin/Role/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }
            var roleViewModel = await _context.FindByIdAsync(id);
            if (roleViewModel == null)
            {
                return NotFound();
            }
            RoleViewModel model = new RoleViewModel
            {
                RoleViewModelId = roleViewModel.Id,
                RoleName = roleViewModel.Name
            };
            return View(model);
        }

        // POST: Admin/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null || _context.Roles == null)
            {
                return Problem("Something is wrong");
            }
            var role = await _context.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            await _context.DeleteAsync(role);
            return RedirectToAction(nameof(Index));
        }

        private bool RoleViewModelExists(string id)
        {
            return (_context.Roles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
