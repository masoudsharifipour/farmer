using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Farmer.Modern.Dto;
using Farmer.Modern.Helper;
using Microsoft.AspNetCore.Identity;

namespace Farmer.Modern.Services.Identity;

public class PermissionService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public PermissionService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<PermissionDto> GetAll(string roleId)
    {
        var model = new PermissionDto();
        var allPermissions = new List<RoleClaimsDto>();
        allPermissions.GetPermissions(typeof(Permissions.PermissionsValue), roleId);
        var role = await _roleManager.FindByIdAsync(roleId);
        model.RoleId = roleId;
        var claims = await _roleManager.GetClaimsAsync(role);
        var allClaimValues = allPermissions.Select(a => a.Value).ToList();
        var roleClaimValues = claims.Select(a => a.Value).ToList();
        var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
        foreach (var permission in allPermissions)
        {
            if (authorizedClaims.Any(a => a == permission.Value))
            {
                permission.Selected = true;
            }
        }

        model.RoleClaims = allPermissions;
        return model;
    }

    public async Task<string> Update(PermissionDto model)
    {
        var role = await _roleManager.FindByIdAsync(model.RoleId);
        var claims = await _roleManager.GetClaimsAsync(role);
        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }

        var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
        foreach (var claim in selectedClaims)
        {
            await _roleManager.AddPermissionClaim(role, claim.Value);
        }

        return model.RoleId;
    }
}