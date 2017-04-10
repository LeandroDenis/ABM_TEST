using Microsoft.Owin.Security;
using Proyecto_ABM.WebSite.App_Start;
using Proyecto_ABM.WebSite.Models;
using Proyecto_ABM.WebSite.Services;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_ABM.WebSite.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new AdAuthenticationService(authenticationManager);
            var authenticationResult = authService.SignIn(model.Username, model.Password);
            if (authenticationResult.IsSuccess)
            {
                return RedirectToAction("Index", "Empleadoes");
            }
            ModelState.AddModelError("errorMsg", authenticationResult.ErrorMessage);
            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Logoff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(MyAuthentication.ApplicationCookie);

            return RedirectToAction("Index", "Login");
        }
    }
}