using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Farmer.Modern.Services.Identity
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public UserService(UserManager<ApplicationUser> userManager, ApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = await _applicationDbContext.Users.ToListAsync();
            return users;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<List<string?>> UserRole(string username)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.UserName == username);
            var role = await _userManager.GetRolesAsync(user);
            return role.ToList();
        }

        public async Task<bool> AddAsync(ApplicationUserInputDto applicationUserInputDto)
        {
            var userAny = await _userManager.FindByNameAsync(applicationUserInputDto.PhoneNumber);
            if (userAny != null)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                UserName = applicationUserInputDto.PhoneNumber,
                PhoneNumber = applicationUserInputDto.PhoneNumber,
                Address = applicationUserInputDto.Address,
                Name = applicationUserInputDto.Name,
                LastName = applicationUserInputDto.LastName
            };
            var result = await _userManager.CreateAsync(user, applicationUserInputDto.Password);
            await _userManager.AddToRoleAsync(user, applicationUserInputDto.Roles.ToString());
            return result.Succeeded;
        }

        public async Task<bool> UpdateAsync(string id, ApplicationUserInputEditDto applicationUserInputDto)
        {
            var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            var userRole = await _userManager.FindByIdAsync(id);
            user.Id = id;
            user.PhoneNumber = applicationUserInputDto.PhoneNumber;
            user.Address = applicationUserInputDto.Address;
            user.Name = applicationUserInputDto.Name;
            user.LastName = applicationUserInputDto.LastName;
            await _applicationDbContext.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                await _userManager.RemoveFromRolesAsync(userRole, roles);
                await _userManager.AddToRoleAsync(userRole, applicationUserInputDto.Roles.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(userRole, applicationUserInputDto.Roles.ToString());
            }

            return true;
        }
    }
}