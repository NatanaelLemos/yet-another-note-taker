using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using SimpleInjector;
using Xamarin.Forms;

namespace NoteTaker.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ServiceLocator.Register<INotebooksService, NotebooksService>(Lifestyle.Singleton);

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
