using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NLemos.Api.Framework.Models;

namespace NLemos.Api.Framework.Extensions.Controllers
{
    public class HateoasBuilder
    {
        private static readonly Lazy<HateoasBuilder> _instance = new Lazy<HateoasBuilder>(() => new HateoasBuilder());

        private Dictionary<Type, Dictionary<string, string>> _cache = new Dictionary<Type, Dictionary<string, string>>();

        private HateoasBuilder()
        {
        }

        public static HateoasBuilder Instance => _instance.Value;

        public Hateoas<T> Build<T>(ControllerBase controller, T value)
        {
            var controllerType = controller.GetType();

            if (!_cache.TryGetValue(controllerType, out var links))
            {
                links = ExtractLinks(controllerType);
                _cache.Add(controllerType, links);
            }

            return new Hateoas<T>(value, links);
        }

        private Dictionary<string, string> ExtractLinks(Type controllerType)
        {
            var links = new Dictionary<string, string>();
            var controllerUrl = GetControllerUrl(controllerType);

            var controllers = controllerType.Assembly.GetTypes().Where(t =>
                t.BaseType == typeof(ControllerBase) &&
                GetControllerUrl(t).StartsWith(controllerUrl))
                .ToList();

            foreach (var controller in controllers)
            {
                foreach (var link in GetControllerLinks(controller))
                {
                    links.Add(link.Key, link.Value);
                }
            }

            return links;
        }

        private string GetControllerUrl(Type controllerType)
        {
            return controllerType.CustomAttributes
                    .FirstOrDefault(c => c.AttributeType == typeof(RouteAttribute))
                    .ConstructorArguments[0].Value.ToString()
                    .Replace("[controller]", controllerType.Name.Replace("Controller", ""));
        }

        private Dictionary<string, string> GetControllerLinks(Type controllerType)
        {
            var links = new Dictionary<string, string>();
            var controllerUrl = GetControllerUrl(controllerType);

            var httpMethods = controllerType.GetMethods()
                .Where(m => m.CustomAttributes.Any(c => IsHttpMethod(c)))
                .ToImmutableList();

            foreach (var method in httpMethods)
            {
                var link = controllerUrl;
                var httpMethod = method.CustomAttributes.FirstOrDefault(attr => IsHttpMethod(attr));

                if (httpMethod.ConstructorArguments.Any())
                {
                    link += $"/{httpMethod.ConstructorArguments[0].Value.ToString()}";
                }

                links.Add($"{controllerType.Name.Replace("Controller", "")}/{method.Name}", link);
            }

            return links;
        }

        private bool IsHttpMethod(CustomAttributeData attribute)
        {
            return attribute.AttributeType == typeof(HttpGetAttribute) ||
                attribute.AttributeType == typeof(HttpPostAttribute) ||
                attribute.AttributeType == typeof(HttpPutAttribute) ||
                attribute.AttributeType == typeof(HttpDeleteAttribute);
        }
    }
}
