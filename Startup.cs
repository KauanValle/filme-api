using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using FilmesAPI.Repositorys;
using FilmesAPI.Services;
using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            services.AddDbContext<FilmeContext>(opts => opts.UseMySQL(_configuration.GetConnectionString("FilmeConnection")));

            JsonConvert.DefaultSettings = () => jsonSettings;

            services
            .AddHealthChecks().Services
            .AddControllers();

            services.AddScoped<IFilmeRepository, FilmeRepository>();
            services.AddScoped<IFilmeService, FilmeService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection()
            .UseRouting()
            .UseEndpoints(endpoints => { 
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions {
                    ResultStatusCodes = {
                        [HealthStatus.Healthy] = StatusCodes.Status200OK,
                        [HealthStatus.Degraded] = StatusCodes.Status200OK,
                        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                    }
                });
            });
        }
    }
}