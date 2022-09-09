using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Farmer.Modern.Helper;
using Farmer.Modern.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Farmer.Modern
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            // using (var scope = host.Services.CreateScope())
            // {
            //     var services = scope.ServiceProvider;
            //     var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            //     var logger = loggerFactory.CreateLogger("app");
            //     try
            //     {
            //         var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            //         var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            //         await Farmer.Modern.Helper.Seeds.DefaultRoles.SeedAsync(userManager, roleManager);
            //         await Farmer.Modern.Helper.Seeds.DefaultUsers.SeedBasicUserAsync(userManager, roleManager);
            //         await Farmer.Modern.Helper.Seeds.DefaultUsers.SeedSuperAdminAsync(userManager, roleManager);
            //         logger.LogInformation("Finished Seeding Default Data");
            //         logger.LogInformation("Application Starting");
            //     }
            //     catch (Exception ex)
            //     {
            //         logger.LogWarning(ex, "An error occurred seeding the DB");
            //     }
            // }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory());

                    builder.ConfigureLogging((hostingContext, logging) =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Information);
                    });
                })
                .ConfigureLogging((hostingContext, logging) =>
                    logging.SetMinimumLevel(hostingContext.HostingEnvironment.IsProduction()
                        ? LogLevel.Error
                        : LogLevel.Trace));
        }
    }
}