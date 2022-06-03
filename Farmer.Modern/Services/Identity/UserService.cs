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


    public async Task<List<string?>> UserRole(string email)
    {
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        var role = await _userManager.GetRolesAsync(user);
        return role.ToList();
    }

    public async Task<bool> AddAsync(ApplicationUserInputDto applicationUserInputDto)
    {
        var userAny = await _userManager.FindByNameAsync(applicationUserInputDto.UserName);
        if (userAny != null)
        {
            return false;
        }

        var user = new ApplicationUser
        {
            Email = applicationUserInputDto.Email,
            UserName = applicationUserInputDto.UserName,
            PhoneNumber = applicationUserInputDto.PhoneNumber,
            Address = applicationUserInputDto.Address,
            Name = applicationUserInputDto.Name,
            LastName = applicationUserInputDto.LastName
        };
        var result = await _userManager.CreateAsync(user, applicationUserInputDto.Password);
        return result.Succeeded;
    }

    public async Task<bool> UpdateAsync(string id, ApplicationUserInputDto applicationUserInputDto)
    {
        var userAny = await _userManager.FindByNameAsync(applicationUserInputDto.UserName);
        if (userAny != null)
        {
            return false;
        }

        var user = new ApplicationUser
        {
            Email = applicationUserInputDto.Email,
            UserName = applicationUserInputDto.UserName,
            PhoneNumber = applicationUserInputDto.PhoneNumber,
            Address = applicationUserInputDto.Address,
            Name = applicationUserInputDto.Name,
            LastName = applicationUserInputDto.LastName
        };
        await _userManager.UpdateAsync(user);
        return true;
    }
}
}

