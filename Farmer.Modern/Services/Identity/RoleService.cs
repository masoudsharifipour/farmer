using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farmer.Modern.Services.Identity
{
    public class RoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<IdentityRole>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }
    
        public async Task AddRole(string? roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName.Trim()));
            }
        }

        public async Task Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);
        }

        public async Task<IdentityRole> GetByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }

        public async Task UpdateAsync(IdentityRole identityRole)
        {
            await _roleManager.UpdateAsync(identityRole);
        }
    }
}

