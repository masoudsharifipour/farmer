using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Farmer.Modern.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUserInputDto user)
        {
           var result = await _userService.AddAsync(user);
           if(result)
               return RedirectToAction(nameof(Index));
           return View(user);
        }
    }
}

