using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farmer.Modern.Areas.Profile.Controllers
{
    [Authorize(Roles = "Basic")]
    public class ProfileController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}