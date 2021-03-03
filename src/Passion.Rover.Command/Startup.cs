using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Passion.Rover.Command.Domain.Services;
using Passion.Rover.Command.Domain.Services.Contracts;
using Passion.Rover.Command.Handlers;
using Passion.Rover.Command.Services;
using Passion.Rover.Command.Services.Repository;
using Passion.Rover.Command.Settings;

namespace Passion.Rover.Command
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.Configure<DatabaseSettings>(
                Configuration.GetSection(nameof(DatabaseSettings)));

            services.AddSingleton<IDatabaseSettings>(x =>
                x.GetRequiredService<IOptions<DatabaseSettings>>().Value);

            services.AddSingleton<IOutboxRepository, OutboxRepository>();
            services.AddSingleton<IRoverRepository, RoverRepository>();
            
            services.AddSingleton<IOutboxService, OutboxService>();
            services.AddSingleton<IRoverService, RoverService>();
            
            services.AddSingleton<ICameraDomainService, CameraDomainService>();
            services.AddSingleton<IMovementDomainService, MovementDomainService>();
            
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(TakeWhatYouSeeCommandHandler));
            services.AddMediatR(typeof(GoGivenLocationCommandHandler));
            
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRoverService roverService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            Task.Factory.StartNew(() => roverService.SendPassionToMars());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
