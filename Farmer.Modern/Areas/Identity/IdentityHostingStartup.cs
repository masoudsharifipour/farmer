using System;
using Farmer.Modern.Helper;
using Farmer.Modern.Models;
using Farmer.Modern.Models.DbContext;
using Farmer.Modern.Services.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Farmer.Modern.Areas.Identity.IdentityHostingStartup))]

namespace Farmer.Modern.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                // services.AddIdentity<IdentityUser, IdentityRole>(options =>
                //     {
                //         options.Password.RequireLowercase = false;
                //         options.Password.RequireUppercase = false;
                //         options.Password.RequireNonAlphanumeric = false;
                //         options.Password.RequireDigit = false;
                //         options.User.RequireUniqueEmail = false;
                //     })
                //     .AddEntityFrameworkStores<ApplicationDbContext>()
                //     .AddDefaultUI()
                //     .AddDefaultTokenProviders();
            });
        }
    }
}