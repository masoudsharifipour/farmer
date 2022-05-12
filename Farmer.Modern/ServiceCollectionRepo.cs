using Farmer.Modern.Helper;
using Farmer.Modern.Models.DbContext;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Farmer.Modern;

public static class ServiceCollectionRepo
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddScoped<PermissionService>();
        services.AddScoped<RoleService>();
    }
}