using ETLSystem.Listener.Workers;
using ETLSystem.Service.Interfaces;
using ETLSystem.Service.Managers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ETLSystem.Listener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
               .ConfigureAppConfiguration((hostingContext, config) =>
               {
                   config.Sources.Clear();

                   config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .AddJsonFile($"appsettings.Development.json",
                                        optional: true, reloadOnChange: true);

                   config.AddEnvironmentVariables();

                   if (args != null)
                   {
                       config.AddCommandLine(args);
                   }
               })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddTransient<IETLManager, ETLManager>();
                    services.AddTransient<IConfigManager, ConfigManager>();
                    services.AddHostedService<ETLWorker>();
                    services.AddAutoMapper(typeof(Program));
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration);
                });
    }
}
