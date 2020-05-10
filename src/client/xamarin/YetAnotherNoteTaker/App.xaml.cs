using System;
using System.IO;
using Xamarin.Forms;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Data;
using YetAnotherNoteTaker.Events;
using YetAnotherNoteTaker.Events.AuthEvents;
using YetAnotherNoteTaker.Events.NotebookEvents;
using YetAnotherNoteTaker.Events.NoteEvents;
using YetAnotherNoteTaker.Events.SettingsEvents;
using YetAnotherNoteTaker.State;
using YetAnotherNoteTaker.Views;

namespace YetAnotherNoteTaker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            RegisterServices();
            StartListeners();
            ApplyTheme();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void RegisterServices()
        {
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            XamarinClientContext.DatabasePath = Path.Combine(localPath, "notetaker.db");

            ServiceLocator.Register<IEventBroker, EventBroker>();
            ServiceLocator.Register<IRestClient, RestClient>();
            ServiceLocator.Register<IUrlBuilder>(new UrlBuilder("http://localhost:5000"));

            ServiceLocator.Register<IAuthRepository, XamarinAuthRepository>();
            ServiceLocator.Register<IAuthService, AuthService>();
            ServiceLocator.Register<AuthEventsListener>();

            ServiceLocator.Register<INotebooksRepository, XamarinNotebooksRepository>();
            ServiceLocator.Register<INotebooksService, NotebooksService>();
            ServiceLocator.Register<NotebookEventsListener>();

            ServiceLocator.Register<INotesRepository, XamarinNotesRepository>();
            ServiceLocator.Register<INotesService, NotesService>();
            ServiceLocator.Register<NoteEventsListener>();

            ServiceLocator.Register<ISettingsRepository, XamarinSettingsRepository>();
            ServiceLocator.Register<ISettingsService, SettingsService>();
            ServiceLocator.Register<SettingsEventsListener>();
        }

        private void StartListeners()
        {
            ServiceLocator.Get<AuthEventsListener>().Start();
            ServiceLocator.Get<NotebookEventsListener>().Start();
            ServiceLocator.Get<NoteEventsListener>().Start();
            ServiceLocator.Get<SettingsEventsListener>().Start();
        }

        private void ApplyTheme()
        {
            Themes.ApplyLightTheme(this);
            var eventBroker = ServiceLocator.Get<IEventBroker>();
            eventBroker.Subscribe<SettingsRefreshResult>(arg =>
            {
                if (arg.Settings.IsDarkMode)
                {
                    Themes.ApplyDarkTheme(this);
                }
                else
                {
                    Themes.ApplyLightTheme(this);
                }

                return eventBroker.Notify(new ListNotebooksCommand());
            });
        }
    }
}
