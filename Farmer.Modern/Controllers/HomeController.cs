using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Microsoft.AspNetCore.Mvc;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Farmer.Modern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
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