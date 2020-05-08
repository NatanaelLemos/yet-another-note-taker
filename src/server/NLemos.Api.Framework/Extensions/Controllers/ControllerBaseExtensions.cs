﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NLemos.Api.Framework.Models;

namespace NLemos.Api.Framework.Extensions.Controllers
{
    public static class ControllerBaseExtensions
    {
        /// <summary>
        /// Gets the email address of the logged user from its request header.
        /// </summary>
        /// <param name="controller">The controller that has the request.</param>
        /// <returns>User's email address.</returns>
        public static string GetUserEmail(this ControllerBase controller)
        {
            var userEmail = controller?.User?.Claims?.FirstOrDefault(c => c.Type.ToLower().Contains("emailaddress"))?.Value;
            return userEmail;
        }

        /// <summary>
        /// Injects <paramref name="value"/> into a HATEOAS container.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="value"/>.</typeparam>
        /// <param name="controller">Controller base from which will read the links.</param>
        /// <param name="value">The result value.</param>
        /// <returns>Value containing links.</returns>
        public static Hateoas<T> HateoasResult<T>(this ControllerBase controller, T value)
        {
            return HateoasProcessor.Instance.Process(controller, value);
        }
    }
}
