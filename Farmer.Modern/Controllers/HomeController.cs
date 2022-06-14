using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Microsoft.AspNetCore.Mvc;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Farmer.Modern.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                var role = await _userManager.GetRolesAsync(currentUser);
                if (role.Contains("Basic")) return Redirect("/Profile/Dashboard");

            }
            var homeDto = new HomeDto();
            var values = await GetCountWorkStatus();
            homeDto.WorkCount = values;
            return View(homeDto);

        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public async Task AddRole(string roleName)
        {
            await Task.FromResult(roleName);
        }

        private async Task<WorkCount> GetCountWorkStatus()
        {
            var dic = new WorkCount();
            var results = _dbContext.Work.AsQueryable();

            dic.All = await results.CountAsync();
            dic.Block = await results.CountAsync(x => x.Status == ActionStatus.Block);
            dic.Done = await results.CountAsync(x => x.Status == ActionStatus.Done);
            dic.Todo = await results.CountAsync(x => x.Status == ActionStatus.Todo);
            dic.InProgress = await results.CountAsync(x => x.Status == ActionStatus.InProgress);

            return dic;
        }
    }
}