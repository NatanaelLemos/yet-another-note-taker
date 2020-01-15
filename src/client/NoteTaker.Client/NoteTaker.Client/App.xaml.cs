﻿using System;
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

        protected override async void OnResume()
        {
            RegisterServices();
            await LoadTheme();
        }

        private void RegisterServices()
        {
            var localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            NoteTakerContext.DatabasePath = Path.Combine(localPath, "notetaker.db");

            ServiceLocator.Register<NoteTakerContext>();
            ServiceLocator.Register<IEventBroker, EventBroker>();

            ServiceLocator.Register<INotebooksRepository, NotebooksRepository>();
            ServiceLocator.Register<INotebooksService, NotebooksService>();
            ServiceLocator.Register<INotebooksAppService, NotebooksAppService>();

            ServiceLocator.Register<INotesRepository, NotesRepository>();
            ServiceLocator.Register<INotesService, NotesService>();
            ServiceLocator.Register<INotesAppService, NotesAppService>();

            ServiceLocator.Register<ISettingsRepository, SettingsRepository>();
            ServiceLocator.Register<ISettingsService, SettingsService>();
            ServiceLocator.Register<ISettingsAppService, SettingsAppService>();

            ServiceLocator.Register<ISyncService, SyncService>();
            ServiceLocator.Register<ISocketSenderAppService, SocketSenderAppService>();
            ServiceLocator.Register<ISocketListenerAppService, SocketListenerAppService>();

            ServiceLocator.Get<INotebooksAppService>().StartListeners();
            ServiceLocator.Get<INotesAppService>().StartListeners();
            ServiceLocator.Get<ISettingsAppService>().StartListeners();
            ServiceLocator.Get<ISocketSenderAppService>().StartListeners();
            ServiceLocator.Get<ISocketListenerAppService>().StartListeners();
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
