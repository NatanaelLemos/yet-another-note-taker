using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Domain;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Services
{
    public interface INotesAppService
    {
        ObservableCollection<Note> DataSource { get; }

        Task FetchAll();

        Task FilterByNotebookId(Guid id);
    }
}
