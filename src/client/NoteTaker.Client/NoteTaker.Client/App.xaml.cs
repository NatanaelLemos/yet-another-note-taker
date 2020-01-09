using System;
using System.IO;
using System.Threading.Tasks;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Client.State.SettingsEvents;
using NoteTaker.Client.Views;
using NoteTaker.Data;
using NoteTaker.Data.Repositories;
using NoteTaker.Domain.Data;
using NoteTaker.Domain.Entities;
using NoteTaker.Domain.Services;
using SimpleInjector;
using Xamarin.Forms;

namespace NoteTaker.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            RegisterServices();
            await LoadTheme();
        }

        protected override void OnSleep()
        {
            ServiceLocator.Clear();
            PageNavigator.ClearHistory();
        }

        protected override async void OnResume()
        {
            RegisterServices();
            await LoadTheme();
        }

        private void RegisterServices()
        {
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            NoteTakerContext.DatabasePath = Path.Combine(localPath, "notetaker.db");

            ServiceLocator.Register<NoteTakerContext>(Lifestyle.Singleton);
            ServiceLocator.Register<IEventBroker, EventBroker>(Lifestyle.Singleton);

            ServiceLocator.Register<INotebooksRepository, NotebooksRepository>(Lifestyle.Singleton);
            ServiceLocator.Register<INotebooksService, NotebooksService>(Lifestyle.Singleton);
            ServiceLocator.Register<INotebooksAppService, NotebooksAppService>(Lifestyle.Singleton);

            ServiceLocator.Register<INotesRepository, NotesRepository>(Lifestyle.Singleton);
            ServiceLocator.Register<INotesService, NotesService>(Lifestyle.Singleton);
            ServiceLocator.Register<INotesAppService, NotesAppService>(Lifestyle.Singleton);

            ServiceLocator.Register<ISettingsRepository, SettingsRepository>(Lifestyle.Singleton);
            ServiceLocator.Register<ISettingsService, SettingsService>(Lifestyle.Singleton);
            ServiceLocator.Register<ISettingsAppService, SettingsAppService>(Lifestyle.Singleton);

            ServiceLocator.Get<INotebooksAppService>().StartListeners();
            ServiceLocator.Get<INotesAppService>().StartListeners();
            ServiceLocator.Get<ISettingsAppService>().StartListeners();
        }

        private async Task LoadTheme()
        {
            var eventBroker = ServiceLocator.Get<IEventBroker>();
            eventBroker.Listen<CreateOrUpdateSettingsCommand>(c =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (c.Settings.DarkMode)
                    {
                        Themes.SetDarkTheme(this);
                    }
                    else
                    {
                        Themes.SetLightTheme(this);
                    }
                });

                return Task.CompletedTask;
            });

            var settings = await eventBroker.Query<SettingsQuery, Settings>(new SettingsQuery());

            if (settings.DarkMode)
            {
                Themes.SetDarkTheme(this);
            }
            else
            {
                Themes.SetLightTheme(this);
            }
        }
    }
}
