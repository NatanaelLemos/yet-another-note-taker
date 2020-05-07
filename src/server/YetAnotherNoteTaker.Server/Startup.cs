using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLemos.Api.Framework.Extensions.Startup;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

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

            AddRavenDB(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app
                .UseErrorHandling()
                .ConfigureBasicApp(env)
                .ConfigureSwagger(AppTitle, AppVersion);
        }

        private void AddRavenDB(IServiceCollection services)
        {
            var store = new DocumentStore
            {
                Urls = Configuration.GetSection("RavenDB:Urls").GetChildren().Select(c => c.Value).ToArray(),
                Database = Configuration.GetSection("RavenDB:DatabaseName").Value
            };

            store.Initialize();
            services.AddSingleton<IDocumentStore>(store);
            services.AddScoped<IAsyncDocumentSession>(
                serviceProvider => serviceProvider
                    .GetService<IDocumentStore>()
                    .OpenAsyncSession());
        }
    }
}
