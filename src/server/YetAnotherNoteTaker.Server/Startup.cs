using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLemos.Api.Framework.Extensions.Startup;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Services;

namespace YetAnotherNoteTaker.Server
{
    public class Startup
    {
        private string AppTitle => Configuration.GetSection("AppTitle").Value;
        private string AppVersion => Configuration.GetSection("AppVersion").Value;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddBasicServices()
                .AddSwagger(AppTitle, AppVersion);

            services.AddSingleton(s =>
                new NoteTakerContext(Configuration.GetConnectionString("Default")));

            services
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddScoped<IUsersService, UsersService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseErrorHandling()
                .ConfigureBasicApp(env)
                .ConfigureSwagger(AppTitle, AppVersion);
        }
    }
}
