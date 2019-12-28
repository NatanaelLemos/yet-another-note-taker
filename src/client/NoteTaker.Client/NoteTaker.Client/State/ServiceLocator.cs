using System;
using SimpleInjector;

namespace NoteTaker.Client.State
{
    public static class ServiceLocator
    {
        private static Lazy<Container> _container = new Lazy<Container>(() => new Container());

        public static void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Value.Register<TService, TImplementation>();
        }

        public static void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            _container.Value.Register<TService, TImplementation>(lifestyle);
        }

        public static TService Get<TService>() where TService : class
        {
            return _container.Value.GetInstance<TService>();
        }
    }
}
