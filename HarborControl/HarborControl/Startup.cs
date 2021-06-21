using HarborControl.Core.Clock;
using HarborControl.Core.Control;
using HarborControl.EntityFramework;
using HarborControl.Interfaces.Repositories;
using HarborControl.Interfaces.Services;
using HarborControl.Interfaces.Vessels;
using HarborControl.Weather;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;

namespace HarborControl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HarborControlContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("SQL")))
                    .AddTransient<IWeatherService, WeatherService>()
                    .AddSingleton<IClockService, ClockService>()
                    .AddTransient<IControlService, ControlService>()
                    .AddTransient<IRepository<IVessel, string>, VesselRepository>()
                    .AddControllers();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSpaStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                    spa.Options.SourcePath = "ClientApp/";
                else
                    spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseVueCli(npmScript: "serve");
                }
            });
        }
    }
}