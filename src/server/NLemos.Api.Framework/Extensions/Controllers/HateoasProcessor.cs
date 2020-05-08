using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using NLemos.Api.Framework.Models;

namespace NLemos.Api.Framework.Extensions.Controllers
{
    public class HateoasProcessor
    {
        private static readonly Lazy<HateoasProcessor> _instance = new Lazy<HateoasProcessor>(() => new HateoasProcessor());

        private Dictionary<Type, Dictionary<string, string>> _cache = new Dictionary<Type, Dictionary<string, string>>();

        private HateoasProcessor()
        {
        }

        public static HateoasProcessor Instance => _instance.Value;

        public Hateoas<T> Process<T>(ControllerBase controller, T value)
        {
            var controllerType = controller.GetType();

            if (!_cache.TryGetValue(controllerType, out var links))
            {
                links = ExtractLinks(controllerType);
                _cache.Add(controllerType, links);
            }

            var hateoas = new Hateoas<T>(value, links);

            return hateoas;
        }

        private Dictionary<string, string> ExtractLinks(Type controllerType)
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
                    link += "/" + httpMethod.ConstructorArguments[0].Value.ToString();
                }

                links.Add(method.Name, link);
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

        private bool IsHttpMethod(CustomAttributeData attribute)
        {
            return attribute.AttributeType == typeof(HttpGetAttribute) ||
                attribute.AttributeType == typeof(HttpPostAttribute) ||
                attribute.AttributeType == typeof(HttpPutAttribute) ||
                attribute.AttributeType == typeof(HttpDeleteAttribute);
        }
    }
}
