using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Helper.Seeds;
using Farmer.Modern.Migrations;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Farmer.Modern.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbContext;

        public UsersController(UserService userService, UserManager<ApplicationUser> userManager,
            ApplicationDbContext dbContext)
        {
            _userService = userService;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var roles = (from object e in Enum.GetValues(typeof(Roles))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();

            ViewBag.Roles = new SelectList(roles, "Value", "Key");


            var users = await _userService.GetUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            ApplicationUserInputDto applicationUserInputDto = new ApplicationUserInputDto();
            var roles = (from object e in Enum.GetValues(typeof(Roles))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();

            ViewBag.Roles = new SelectList(roles, "Value", "Key");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ApplicationUserInputDto user)
        {
            var result = await _userService.AddAsync(user);
            if (result)
                return RedirectToAction(nameof(Index));
            return View(user);
        }


        // GET: user/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            var userInputDto = new ApplicationUserInputEditDto();
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);
            var rolesStatus = (from object e in Enum.GetValues(typeof(Roles))
                select new KeyValuePair<string, int>(e.ToString(), (int) e)).ToList();
            var role = await _userManager.GetRolesAsync(user);
            if (role.Any())
            {
                var value = (int) Enum.Parse(typeof(Roles), role.FirstOrDefault() ?? string.Empty);
                ViewBag.Roles = new SelectList(rolesStatus, "Value", "Key", value);
            }
            else
            {
                ViewBag.Roles = new SelectList(rolesStatus, "Value", "Key");
            }


            userInputDto.Id = user.Id;
            userInputDto.Address = user.Address;
            userInputDto.Name = user.Name;
            userInputDto.LastName = user.LastName;
            userInputDto.PhoneNumber = user.PhoneNumber;

            if (user == null)
            {
                return NotFound();
            }

            return View(userInputDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,
            ApplicationUserInputEditDto applicationUserInputDto)
        {
            if (id != applicationUserInputDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.UpdateAsync(id, applicationUserInputDto);
                }
                catch (Exception exception)
                {
                    throw new Exception("Error On update user");
                }

                return RedirectToAction(nameof(Index));
            }

            return View(applicationUserInputDto);
        }


        public async Task<IActionResult> Details(string? id)
        {
            var userInputDto = new ApplicationUserInputEditDto();

            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetById(id);
            userInputDto.Id = user.Id;
            userInputDto.Address = user.Address;
            userInputDto.Name = user.Name;
            userInputDto.LastName = user.LastName;
            userInputDto.PhoneNumber = user.PhoneNumber;
            if (user == null)
            {
                return NotFound();
            }

            return View(userInputDto);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}