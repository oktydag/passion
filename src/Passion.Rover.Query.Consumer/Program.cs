using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Passion.Rover.Query.Consumer.Services;
using Passion.Rover.Query.Consumer.Services.Contracts;

namespace Passion.Rover.Query.Consumer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            
            var busService = serviceProvider.GetService<IBusService>();
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

            services.AddSingleton<IBusService, BusService>();
            services.AddSingleton<IElasticsearchService, ElasticsearchService>();
        }
    }
}
