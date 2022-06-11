using System;
using System.Net;
using System.Threading.Tasks;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Farmer.Modern.Controllers
{
    [Authorize("Admin,SuperAdmin")]
    public class RolesController : Controller
    {
        private readonly RoleService _roleService;
    
        public RolesController(RoleService roleService)
        {
            _roleService = roleService;
        }
    
        public async Task<IActionResult> Index()
        {
            var roles = await _roleService.GetAll();
            return View(roles);
        }
    
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleService.AddRole(roleName);
            }
    
            return RedirectToAction("Index");
        }
    
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var role = await _roleService.GetByIdAsync(id);
            return View(role);
        }
    
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
              return  BadRequest("id is null");
    
            }          
            var role = await _roleService.GetByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
    
        }
    
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAsync(IdentityRole identityRole)
        {
            if (identityRole != null)
            {
                await _roleService.UpdateAsync(identityRole);
            }
    
            return RedirectToAction("Index");
        }
    
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id != null)
            {
                await _roleService.Delete(id);
            }
    
            return RedirectToAction("Index");
        }
    }
}

