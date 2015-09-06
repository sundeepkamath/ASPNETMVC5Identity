using ASPNetMVC5Identity.App_Start;
using ASPNetMVC5Identity.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security;

namespace ASPNetMVC5Identity.Controllers
{
    public class AuthController : Controller
    {
        private  AppUserManager _userManager;

        public AuthController(AppUserManager userManager)
        {
            UserManager = userManager;
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var model = new LoginModel
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = await UserManager.FindAsync(model.Email, model.Password);

            if (user != null)
            {
                var identity = await UserManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }

            // In case user authentication fails.
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            return authManager;
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("index", "home");
            }

            return returnUrl;
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("index","Home");
        }
    }
}