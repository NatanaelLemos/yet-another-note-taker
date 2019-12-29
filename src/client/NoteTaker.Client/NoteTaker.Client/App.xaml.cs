using System;
using System.IO;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Data;
using NoteTaker.Data.Repositories;
using NoteTaker.Domain.Data;
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

            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            NoteTakerContext.DatabasePath = Path.Combine(localPath, "notetaker.db");

            ServiceLocator.Register<NoteTakerContext>(Lifestyle.Singleton);
            ServiceLocator.Register<INotebooksRepository, NotebooksRepository>(Lifestyle.Singleton);
            ServiceLocator.Register<INotebooksService, NotebooksService>(Lifestyle.Singleton);

            ServiceLocator.Register<INotebooksAppService, NotebooksAppService>(Lifestyle.Singleton);
            ServiceLocator.Register<INotesAppService, NotesAppService>(Lifestyle.Singleton);

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
