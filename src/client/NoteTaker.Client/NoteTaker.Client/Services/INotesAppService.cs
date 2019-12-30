using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoteTaker.Client.Helpers;
using NoteTaker.Domain.Dtos;

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
