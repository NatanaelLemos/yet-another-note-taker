using System;
using Microsoft.Extensions.DependencyInjection;

namespace Observatron
{
    /// <summary>
    /// <see cref="IServiceCollection"/> extensions to inject an <see cref="IEventBroker"/>.
    /// </summary>
    public static class ObservatronExtensions
    {
        /// <summary>
        /// Adds an instance of <see cref="IEventBroker"/> to the service collection.
        /// </summary>
        /// <param name="services">
        ///     Service collection to be injected with the instance of <see cref="IEventBroker"/>.
        /// </param>
        /// <param name="options">Options to configure the instance.</param>
        /// <returns>Injected <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddObservatron(
            this IServiceCollection services,
            Action<ObservatronOptions> options = null)
        {
            var optionsInstance = new ObservatronOptions();
            var eventBroker = ObservatronBuilder.Build(optionsInstance, options);
            services.AddSingleton(eventBroker);
            return services;
        }
    }
}
