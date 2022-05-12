using Farmer.Modern.Helper;
using Farmer.Modern.Models.DbContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Farmer.Modern;

public static class ServiceCollectionIdentity
{
    public static void AddIdentity(this IServiceCollection services)
    {
        // services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        // services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}