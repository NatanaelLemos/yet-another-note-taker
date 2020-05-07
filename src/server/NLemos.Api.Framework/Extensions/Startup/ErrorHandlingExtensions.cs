using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLemos.Api.Framework.Exceptions;

namespace NLemos.Api.Framework.Extensions.Startup
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                await HandleExceptionAsync(context, exception);
            }));
            return app;
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = Error(exception);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)error.status;

            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
            context.Response.Headers.Add("Access-Control-Max-Age", "86400");

            await context.Response.WriteAsync(error.message);
        }

        private static (HttpStatusCode status, string message) Error(Exception ex)
        {
            switch (ex)
            {
                case InvalidModelStateException paramsEx:
                    return (HttpStatusCode.UnprocessableEntity, paramsEx?.InnerException?.Message);

                case InvalidParametersException paramsEx:
                    return (HttpStatusCode.UnprocessableEntity, paramsEx?.InnerException?.Message);

                case KeyNotFoundException keyException:
                    return (HttpStatusCode.NotFound, JsonMessage(keyException?.Message));

                case ArgumentException argEx:
                    return (HttpStatusCode.InternalServerError, JsonMessage(argEx?.Message));

                default:
                    return (HttpStatusCode.InternalServerError, JsonMessage("Internal server error"));
            }
        }

        private static string FullErrorMessage(Exception e)
        {
            var msg = "";

            while (e != null)
            {
                msg += e.Message + "\r\n";
                e = e.InnerException;
            }

            return msg;
        }

        private static string JsonMessage(string message)
        {
            return "{\"error\": \"" + message + "\"}";
        }
    }
}
