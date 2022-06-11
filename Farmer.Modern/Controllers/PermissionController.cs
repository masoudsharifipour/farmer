using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farmer.Modern.Controllers
{
    [Authorize("Admin,SuperAdmin")]
    public class PermissionController : Controller
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        public async Task<IActionResult> Index(string roleId)
        {
            var permissions = await _permissionService.GetAll(roleId);
            return View(permissions);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PermissionDto model)
        {
            var permission = await _permissionService.Update(model);
            return RedirectToAction("Index", new {roleId = permission});
        }
    }
}

