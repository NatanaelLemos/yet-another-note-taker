using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoteTaker.Client.Helpers;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Services
{
    public interface INotebooksAppService
    {
        ObservableCollection<NotebookDto> DataSource { get; }

        BindableObject<NotebookDto> Current { get; }

        Task FetchAll();

        void NewNotebook();

        Task LoadNotebook(Guid id);
        Task Delete(Guid notebookId);
    }
}
