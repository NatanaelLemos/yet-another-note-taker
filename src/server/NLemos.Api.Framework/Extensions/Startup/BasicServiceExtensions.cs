﻿using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace NLemos.Api.Framework.Extensions.Startup
{
    public static class BasicServiceExtensions
    {
        public static IServiceCollection AddBasicServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            services.AddResponseCompression(opt =>
            {
                opt.Providers.Add<GzipCompressionProvider>();
                opt.EnableForHttps = true;
            });

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                //Force to use the ErrorHandling Middleware instead of default Asp.Net
                opt.SuppressModelStateInvalidFilter = true;
            });

            return services;
        }

        public static IServiceCollection RemoveService<TService>(this IServiceCollection services)
        {
            var serviceDescriptor = services.FirstOrDefault(s => s.ServiceType == typeof(TService));
            services.Remove(serviceDescriptor);
            return services;
        }

        public static IApplicationBuilder ConfigureBasicApp(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseResponseCompression();

            return app;
        }
    }
}
