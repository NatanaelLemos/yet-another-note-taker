﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Client.Services;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NotebookEvents;
using NoteTaker.Domain.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NoteTaker.Client.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotebooksPage : ContentPage
    {
        private readonly ObservableCollection<NotebookDto> _dataSource;
        private readonly IEventBroker _eventBroker;

        public NotebooksPage()
        {
            InitializeComponent();

            _dataSource = new ObservableCollection<NotebookDto>();
            _eventBroker = ServiceLocator.Get<IEventBroker>();
            lsvNotebooks.ItemsSource = _dataSource;
            lsvNotebooks.ItemTapped += LsvNotebooks_ItemTapped;

            _eventBroker.Listen<CreateNotebookCommand>(c =>
            {
                _dataSource.Add(c.Dto);
                return Task.CompletedTask;
            });

            _eventBroker.Listen<UpdateNotebookCommand>(c =>
            {
                var itemToUpdate = _dataSource.FirstOrDefault(d => d.Id == c.Dto.Id);

                if(itemToUpdate == null || itemToUpdate.Name == c.Dto.Name)
                {
                    return Task.CompletedTask;
                }

                _dataSource.Remove(itemToUpdate);

                _dataSource.Add(new NotebookDto
                {
                    Id = c.Dto.Id,
                    Name = c.Dto.Name
                });

                return Task.CompletedTask;
            });

            _eventBroker.Listen<DeleteNotebookCommand>(c =>
            {
                var itemToRemove = _dataSource.FirstOrDefault(d => d.Id == c.Dto.Id);

                if (itemToRemove != null)
                {
                    _dataSource.Remove(itemToRemove);
                }

                return Task.CompletedTask;
            });
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            _dataSource.Clear();
            var data = await _eventBroker.Query<NotebookQuery, ICollection<NotebookDto>>(new NotebookQuery());

            foreach (var item in data)
            {
                _dataSource.Add(item);
            }
        }

        private void LsvNotebooks_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var notebookDto = e?.Item as NotebookDto;
            PageNavigator.NavigateTo<NotesPage>(notebookDto);
        }

        private void btnAllNotes_OnClick(object sender, EventArgs e)
        {
            lsvNotebooks.SelectedItem = null;
            PageNavigator.NavigateTo<NotesPage>();
        }

        private void btnNewNotebook_OnClick(object sender, EventArgs e)
        {
            lsvNotebooks.SelectedItem = null;
            PageNavigator.NavigateTo<NotebookEditorPage>();
        }

        private void btnEditNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            PageNavigator.NavigateTo<NotebookEditorPage>(notebookId);
        }

        private async void btnRemoveNotebook_OnClick(object sender, EventArgs e)
        {
            var notebookId = (sender as Button)?.BindingContext as Guid?;
            if (notebookId == null)
            {
                return;
            }

            var answer = await DisplayAlert("Remove", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await _eventBroker.Command(
                    new DeleteNotebookCommand(
                        _dataSource.FirstOrDefault(d => d.Id == notebookId)));
            }
        }
    }
}
