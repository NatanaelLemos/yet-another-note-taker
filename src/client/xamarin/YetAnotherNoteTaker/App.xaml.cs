using System;
using System.IO;
using NLemos.Xamarin.Common.State;
using N2tl.Observer;
using Xamarin.Forms;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Events;
using YetAnotherNoteTaker.Client.Common.Events.AuthEvents;
using YetAnotherNoteTaker.Client.Common.Events.NotebookEvents;
using YetAnotherNoteTaker.Client.Common.Events.NoteEvents;
using YetAnotherNoteTaker.Client.Common.Events.SettingsEvents;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Data;
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

            ServiceLocator.Register<IUserState, UserState>();
            ServiceLocator.Register(
                ObserverBuilder.Build(opt =>
                    opt.AddGeneralInterrupter(t => ServiceLocator.Get<IUserState>().IsAuthenticated(t))));

            ServiceLocator.Register<IPageNavigator>(new PageNavigator(t => ServiceLocator.Get<IUserState>().IsAuthenticated(t)));
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
