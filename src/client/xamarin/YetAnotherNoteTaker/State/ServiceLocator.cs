using SimpleInjector;

namespace YetAnotherNoteTaker.State
{
    public static class ServiceLocator
    {
        private static Container _container;

        public static void Register<TService>()
            where TService : class
        {
            GetContainer().Register<TService>(Lifestyle.Singleton);
        }

        public static void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            GetContainer().Register<TService, TImplementation>(Lifestyle.Singleton);
        }

        public static TService Get<TService>() where TService : class
        {
            return GetContainer().GetInstance<TService>();
        }

        public static void Clear()
        {
        }

        private static Container GetContainer()
        {
            if (_container == null)
            {
                _container = new Container();
            }

            return _container;
        }
    }
}
