using System;

namespace Observatron
{
    /// <summary>
    /// Builder for <see cref="IEventBroker"/>.
    /// </summary>
    public static class ObservatronBuilder
    {
        /// <summary>
        /// Builds an instance of <see cref="IEventBroker"/>.
        /// </summary>
        /// <param name="options">Options to configure the instance.</param>
        /// <returns>Instance of <see cref="IEventBroker"/>.</returns>
        public static IEventBroker Build(Action<ObservatronOptions> options = null)
        {
            var optionsInstance = new ObservatronOptions();
            return Build(optionsInstance, options);
        }

        internal static IEventBroker Build(
            ObservatronOptions optionsInstance,
            Action<ObservatronOptions> options)
        {
            options?.Invoke(optionsInstance);
            var eventBroker = new EventBroker(optionsInstance.Interrupters);
            return eventBroker;
        }
    }
}
