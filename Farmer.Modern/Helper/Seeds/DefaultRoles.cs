using System.Threading.Tasks;
using Farmer.Modern.Models;
using Microsoft.AspNetCore.Identity;

namespace Farmer.Modern.Helper.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
        }
    }


    public enum Roles
    {
        SuperAdmin = 1,
        Admin = 2,
        Basic = 3
    }
}