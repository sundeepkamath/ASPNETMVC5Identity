using System.Security.Claims;
using System.Web.Mvc;

namespace ASPNetMVC5Identity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            ViewBag.Country = claimsIdentity.FindFirst(ClaimTypes.Country).Value;
            return View();
        }
    }
}