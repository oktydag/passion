using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Passion.Rover.Process.Consumer.Services;
using Passion.Rover.Process.Consumer.Services.Contracts;

namespace Passion.Rover.Process.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            
            var busService = serviceProvider.GetService<IBusConfiguratorService>();
            busService.Start();
            
            Console.WriteLine("Listening Rover Commands...");
            Console.ReadLine();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            services.Configure<DatabaseSettings>(configuration.GetSection(nameof(DatabaseSettings)));
            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
            
            services.AddSingleton<IRoverService, RoverService>();
            services.AddSingleton<IBusConfiguratorService, BusConfiguratorService>();
        }
    }
}