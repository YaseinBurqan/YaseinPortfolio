using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YaseinPortfolio.Models.ViewModels;

namespace YaseinPortfolio.Controllers
{
    public class AccountController : Controller
    {

        #region InjectionAndObjects
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> _userManager,
            SignInManager<IdentityUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }
        #endregion

        #region Users

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Mobile
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return View(model);

            }
            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //P@$$w0rd

                var result = await signInManager.PasswordSignInAsync
                        (model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid User or password");
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

    }
}
