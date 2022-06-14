using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farmer.Modern.Areas.Profile.Controllers
{
    [Area("Profile")]
    [Route("Profile/Dashboard")]
    [Authorize(Roles = "Basic")]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ProfileController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _context = dbContext;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<WorkDto> workDtos = new List<WorkDto>();
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            var work = await _context.Work.Where(x => x.AgentId.ToString() == currentUser.Id).Include(x => x.Category)
                .Include(x => x.Garden)
                .Include(x => x.Product)
                .Include(x => x.WaterMotor).ToListAsync();

            foreach (var item in work)
            {
                var agentFullName = await _context.Users.FirstOrDefaultAsync(x => x.Id == item.AgentId.ToString());
                var creatorUser =
                    await _context.Users.FirstOrDefaultAsync(x => x.Id == item.CreatorUserId.ToString());

                workDtos.Add(new WorkDto
                {
                    Id = item.Id,
                    ActionDateTime = item.ActionDatetime,
                    Agent = item.AgentId,
                    CategoryName = item.Category.Name,
                    CategoryId = item.CategoryId,
                    CreatorFullName = $"{creatorUser?.Name} {creatorUser?.LastName}",
                    FullNameAgent = $"{agentFullName?.Name} {agentFullName?.LastName}",
                    Status = item.Status,
                    EndActionDateTime = item.EndActionDateTime,
                    GardenName = item.Garden.Name,
                    ProductName = item.Product.Name,
                    WaterMotorName = item.WaterMotor?.Name,
                    Description = item.Description,
                    Type = item.Type,
                    Size = item.Size,
                });
            }

            return View(workDtos);
        }
    }
}