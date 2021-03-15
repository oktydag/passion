using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Passion.Outbox.Publisher.Events;
using Passion.Outbox.Publisher.Services;
using Passion.Outbox.Publisher.Services.Contracts;
using Passion.Outbox.Publisher.Settings;

namespace Passion.Outbox.Publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            await serviceProvider.GetService<PublishWorker>().Run(args);
            await serviceProvider.GetService<ConsumerWorker>().Run(args);
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
            services.Configure<ProcessSettings>(configuration.GetSection(nameof(ProcessSettings)));

            services.AddSingleton<IDatabaseSettings, DatabaseSettings>();
            services.AddSingleton<IProcessSettings, ProcessSettings>();

            services.AddSingleton<IEventFactory, EventFactory>();
            services.AddSingleton<IMessageBusService, MessageBusService>();
            services.AddSingleton<IOutboxService, OutboxService>();

            services.AddTransient<PublishWorker>();
            services.AddTransient<ConsumerWorker>();
        }
    }
}