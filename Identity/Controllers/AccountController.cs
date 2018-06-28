using System.Security.Claims;
using System.Threading.Tasks;
using AspNetSecurity_m3.Data;
using AspNetSecurity_m3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace AspNetSecurity_m3.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ConfArchUser> userManager;
        private readonly SignInManager<ConfArchUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController(UserManager<ConfArchUser> userManager, SignInManager<ConfArchUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Error");

            var user = new ConfArchUser() {UserName = model.Email, Email = model.Email, BirthDate = model.BirthDate};
            var result = await userManager.CreateAsync(
                user , model.Password);

            if (!await roleManager.RoleExistsAsync("Organizer"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Organizer" });
            if (!await roleManager.RoleExistsAsync("Speaker"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Speaker" });

            await userManager.AddToRoleAsync(user, model.Role);
            await userManager.AddClaimAsync(user, new Claim("technology", model.Technology));

            if (result.Succeeded)
                return View("RegistrationConfirmation");
            
            foreach (var error in result.Errors)
                ModelState.AddModelError("error", error.Description);
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result =
                    await
                        signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                            lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);
                if (result.RequiresTwoFactor)
                {
                    //
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return View("LoggedOut");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Conference");
        }

    }
}
