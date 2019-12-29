using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoteTaker.Domain;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;

namespace NoteTaker.Client.Services
{
    public interface INotebooksAppService
    {
        ObservableCollection<NotebookDto> DataSource { get; }

        Task FetchAll();

        Task Create(NewNotebookDto notebook);
    }
}
