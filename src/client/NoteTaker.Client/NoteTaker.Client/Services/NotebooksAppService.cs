using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;
using System.Timers;
using System.Collections.Generic;
using NoteTaker.Client.Helpers;
using System;
using System.Linq;

namespace NoteTaker.Client.Services
{
    public class NotebooksAppService : INotebooksAppService
    {
        private readonly INotebooksService _service;
        private readonly Timer _updateTimer;
        private readonly List<NotebookDto> _currentUpdates;

        public NotebooksAppService(INotebooksService service)
        {
            _service = service;

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += UpdateTimer_Elapsed;
            _updateTimer.Start();

            _currentUpdates = new List<NotebookDto>();

            DataSource = new ObservableCollection<NotebookDto>();
            Current = new BindableObject<NotebookDto>();
            Current.OnDtoChanged += Current_OnDtoChanged;
        }

        public ObservableCollection<NotebookDto> DataSource { get; private set; }

        public BindableObject<NotebookDto> Current { get; private set; }

        public async Task FetchAll()
        {
            var data = await _service.GetAll();
            DataSource.Clear();

            foreach (var item in data)
            {
                DataSource.Add(item);
            }
        }

        public void NewNotebook()
        {
            Current.UpdateObject(new NotebookDto());
        }

        public async Task LoadNotebook(Guid notebookId)
        {
            var notebook = await _service.GetById(notebookId);
            Current.UpdateObject(notebook);
        }

        public Task Delete(Guid notebookId)
        {
            var notebook = DataSource.FirstOrDefault(d => d.Id == notebookId);

            if(notebook == null)
            {
                return Task.CompletedTask;
            }

            DataSource.Remove(notebook);
            return _service.Delete(notebook);
        }

        private void Current_OnDtoChanged(NotebookDto dto)
        {
            _currentUpdates.Add(dto);
            UpdateDataSourceWithCurrent();
        }

        private async void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!_currentUpdates.Any())
            {
                return;
            }

            _updateTimer.Stop();

            try
            {
                var response = await _service.CreateOrUpdate(_currentUpdates.LastOrDefault());
                Current.Dto.Id = response.Id;

                lock (_currentUpdates)
                {
                    _currentUpdates.Clear();
                }
            }
            finally
            {
                _updateTimer.Start();
            }
        }

        private void UpdateDataSourceWithCurrent()
        {
            if(Current.Dto.Id == Guid.Empty)
            {
                return;
            }

            var existing = DataSource.Where(d => d.Id == Current.Dto.Id).FirstOrDefault();

            if (existing == null)
            {
                DataSource.Add(Current.Dto);
                return;
            }

            if(existing.Name != Current.Dto.Name)
            {
                DataSource.Remove(existing);
                DataSource.Add(Current.Dto);
            }
        }
    }
}
