using System;
using SimpleInjector;

namespace NoteTaker.Client.State
{
    public static class ServiceLocator
    {
        private static Container _container = new Container();

        public static void Register<TService>()
            where TService : class
        {
            _container.Register<TService>();
        }

        public static void Register<TService>(Lifestyle lifestyle)
            where TService : class
        {
            _container.Register<TService>(lifestyle);
        }

        public static void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
        }

        public static void Register<TService, TImplementation>(Lifestyle lifestyle)
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(lifestyle);
        }

        public static TService Get<TService>() where TService : class
        {
            return _container.GetInstance<TService>();
        }

        public static void Clear()
        {
            _container.Dispose();
            _container = new Container();
        }
    }
}
