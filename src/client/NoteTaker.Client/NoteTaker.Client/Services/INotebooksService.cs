using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoteTaker.Domain;

namespace NoteTaker.Client.Services
{
    public interface INotebooksService
    {
        ObservableCollection<Notebook> DataSource { get; }

        Task FetchAll();

        Task Create(Notebook notebook);
    }
}
