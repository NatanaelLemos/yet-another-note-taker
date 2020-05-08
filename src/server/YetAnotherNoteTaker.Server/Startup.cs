using System;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLemos.Api.Framework.Extensions.Startup;
using YetAnotherNoteTaker.Server.Data;
using YetAnotherNoteTaker.Server.Security;
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

            services
                .AddScoped<INotebooksRepository, NotebooksRepository>()
                .AddScoped<INotebooksService, NotebooksService>();

            services
                .AddScoped<IResourceOwnerPasswordValidator, PasswordValidator>()
                .AddScoped<IProfileService, ProfileService>();

            services
                .AddIdentityServer(o =>
                {
                    o.Authentication.CookieLifetime = new TimeSpan(360, 0, 0, 0);
                    o.Authentication.CookieSlidingExpiration = false;
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(SecurityConfig.GetApis(Configuration))
                .AddInMemoryIdentityResources(SecurityConfig.GetIdentityResources())
                .AddInMemoryClients(SecurityConfig.GetClients(Configuration))
                .Services.AddTransient<ICorsPolicyService>(p =>
                {
                    var corsService = new DefaultCorsPolicyService(
                        p.GetRequiredService<ILogger<DefaultCorsPolicyService>>());
                    corsService.AllowAll = true;
                    return corsService;
                });

            AddAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseErrorHandling()
                .UseIdentityServer()
                .UseAuthentication()
                .ConfigureBasicApp(env)
                .ConfigureSwagger(AppTitle, AppVersion);
        }

        private void AddAuthentication(IServiceCollection services)
        {
            var authUrl = Configuration.GetSection("AuthClient:Authority").Value;
            var audience = Configuration.GetSection("AuthClient:ScopeName").Value;
            services.AddAuthentication(authUrl, audience);
        }
    }
}
