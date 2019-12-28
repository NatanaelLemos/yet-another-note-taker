using System;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly INotebooksService _service;

        public NotebookEditorPage()
        {
            InitializeComponent();
            _service = ServiceLocator.Get<INotebooksService>();
            boxNotebook.SetDynamicWidth();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Title = "New notebook";
        }

        private void Cancel_Clicked(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            _service.Create(new Notebook { Name = txtNotebook.Text });
            PageNavigator.Back();
        }
    }
}
