using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Domain;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Entities;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotebooksAppService : INotebooksAppService
    {
        private readonly INotebooksService _service;

        public NotebooksAppService(INotebooksService service)
        {
            _service = service;
            DataSource = new ObservableCollection<NotebookDto>();
        }

        public ObservableCollection<NotebookDto> DataSource { get; private set; }

        public async Task FetchAll()
        {
            var data = await _service.GetAll();
            DataSource.Clear();

            foreach (var item in data)
            {
                DataSource.Add(item);
            }
        }

        public async Task Create(NewNotebookDto notebook)
        {
            var dto = await _service.Create(notebook);
            DataSource.Add(dto);
        }
    }
}
