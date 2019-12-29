using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Services
{
    public class NotesAppService : INotesAppService
    {
        public NotesAppService()
        {
            DataSource = new ObservableCollection<Note>();
        }

        public ObservableCollection<Note> DataSource { get; }

        public Task FetchAll()
        {
            DataSource.Clear();
            DataSource.Add(new Note
            {
                Name = "Bla"
            });
            DataSource.Add(new Note
            {
                Name = "Bla 2"
            });

            return Task.CompletedTask;
        }

        public Task FilterByNotebookId(Guid id)
        {
            DataSource.Clear();
            DataSource.Add(new Note
            {
                Name = "Bla 1"
            });

            return Task.CompletedTask;
        }
    }
}
