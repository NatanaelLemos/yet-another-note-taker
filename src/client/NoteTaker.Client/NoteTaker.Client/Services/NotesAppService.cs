using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using NoteTaker.Client.Helpers;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotesAppService : INotesAppService
    {
        private readonly INotesService _service;
        private readonly Timer _updateTimer;
        private readonly List<NoteDetailDto> _currentUpdates;

        public NotesAppService(INotesService service)
        {
            _service = service;

            _updateTimer = new Timer(1000);
            _updateTimer.Elapsed += UpdateTimer_Elapsed;
            _updateTimer.Start();

            _currentUpdates = new List<NoteDetailDto>();

            DataSource = new ObservableCollection<NoteListItemDto>();
            Current = new BindableObject<NoteDetailDto>();
            Current.OnDtoChanged += Current_OnDtoChanged;
        }

        public ObservableCollection<NoteListItemDto> DataSource { get; private set; }

        public BindableObject<NoteDetailDto> Current { get; private set; }

        public async Task FetchAll()
        {
            var data = await _service.GetAll();
            DataSource.Clear();

            foreach (var item in data)
            {
                DataSource.Add(item);
            }
        }

        public async Task FilterByNotebookId(Guid id)
        {
            var data = await _service.FindByNotebookId(id);
            DataSource.Clear();

            foreach (var item in data)
            {
                DataSource.Add(item);
            }
        }

        public void NewNote(Guid notebookId)
        {
            Current.UpdateObject(new NoteDetailDto { NotebookId = notebookId });
        }

        public async Task LoadNote(Guid noteId)
        {
            var note = await _service.GetById(noteId);
            Current.UpdateObject(note);
        }

        private void Current_OnDtoChanged(NoteDetailDto dto)
        {
            _currentUpdates.Add(dto);
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
    }
}
