using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using NPress.Core.Domains;
using NPress.Web.Models;

namespace NPress.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> m_signInManager;

        public AuthController(SignInManager<User> signInManager)
        {
            m_signInManager = signInManager;
        }

        [Route("~/admin/login")]
        public IActionResult Login(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost("~/admin/login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if(ModelState.IsValid)
            {
                var result = await m_signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            return View();
        }

        [Route("~/admin/logout")]
        public async Task<IActionResult> Logout()
        {
            await m_signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if(!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("index", "dashboard", new { area = "admin" });
            }
        }
    }
}
