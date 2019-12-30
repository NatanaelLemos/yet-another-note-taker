using System;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly INotebooksAppService _service;

        public NotebookEditorPage()
        {
            InitializeComponent();
            _service = ServiceLocator.Get<INotebooksAppService>();
            boxNotebook.SetDynamicWidth();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.Title = "New notebook";
        }

        private void btnCancel_OnClick(object sender, EventArgs e)
        {
            PageNavigator.Back();
        }

        private void btnSave_OnClick(object sender, EventArgs e)
        {
            _service.Create(new NewNotebookDto { Name = txtNotebook.Text });
            PageNavigator.Back();
        }
    }
}
