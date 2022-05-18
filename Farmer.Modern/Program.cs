using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Farmer.Modern;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
        CultureInfo info = new CultureInfo("fa-Ir");
//set Persian option to specified culture
        info.DateTimeFormat.Calendar = new PersianCalendar();
        Thread.CurrentThread.CurrentCulture = info;
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder =>
            {
                builder.UseStartup<Startup>();
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