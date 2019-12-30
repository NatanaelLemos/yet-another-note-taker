using System;
using NoteTaker.Client.Extensions;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebookEditorPage : ContentPage
    {
        private readonly INotebooksAppService _service;
        private readonly Guid? _id;

        public NotebookEditorPage()
        {
            InitializeComponent();
            boxNotebook.SetDynamicWidth();

            _service = ServiceLocator.Get<INotebooksAppService>();
            _service.Current.Bind(nameof(NotebookDto.Name), txtNotebook);
        }

        public NotebookEditorPage(Guid id)
            : this()
        {
            _id = id;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_id == null || _id == Guid.Empty)
            {
                this.Title = "New notebook";
                _service.NewNotebook();
            }
            else
            {
                await _service.LoadNotebook(_id.Value);
                this.Title = _service.Current.Dto.Name;
            }
        }
    }
}
