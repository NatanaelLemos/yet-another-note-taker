using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Domain;

namespace NoteTaker.Client.Services
{
    public class NotebooksService : INotebooksService
    {
        public NotebooksService()
        {
            DataSource = new ObservableCollection<Notebook>();
        }

        public ObservableCollection<Notebook> DataSource { get; private set; }

        public Task FetchAll()
        {
            if (!DataSource.Any())
            {
                DataSource.Add(new Notebook { Id = Guid.NewGuid(), Name = "Notebook 1" });
                DataSource.Add(new Notebook { Id = Guid.NewGuid(), Name = "Notebook 2" });
            }

            return Task.CompletedTask;
        }

        public Task Create(Notebook notebook)
        {
            DataSource.Add(notebook);
            return Task.CompletedTask;
        }
    }
}
