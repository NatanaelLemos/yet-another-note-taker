using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.Helpers;
using NoteTaker.Domain;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Services
{
    public interface INotesAppService
    {
        ObservableCollection<NoteListItemDto> DataSource { get; }

        BindableObject<NoteDetailDto> Current { get; }

        Task FetchAll();

        Task FilterByNotebookId(Guid id);

        void NewNote(Guid notebookId);

        Task LoadNote(Guid noteId);
    }
}
