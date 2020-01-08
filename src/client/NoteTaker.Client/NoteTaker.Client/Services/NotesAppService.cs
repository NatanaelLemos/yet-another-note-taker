using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;
using System.Timers;

namespace NoteTaker.Client.Services
{
    public class NotesAppService : INotesAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotesService _service;

        private readonly Queue<Task> _taskQueue;
        private readonly Timer _updateTimer;

        public NotesAppService(IEventBroker eventBroker, INotesService service)
        {
            _eventBroker = eventBroker;
            _service = service;
            _taskQueue = new Queue<Task>();

            _updateTimer = new Timer(500);
            _updateTimer.Elapsed += _updateTimer_Elapsed;
            _updateTimer.Start();
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateNoteCommand>(CreateNoteCommandHandler);
            _eventBroker.Listen<UpdateNoteCommand>(UpdateNoteCommandHandler);
            _eventBroker.Listen<DeleteNoteCommand>(DeleteNoteCommandHandler);

            _eventBroker.Listen<NoteQuery, ICollection<NoteDto>>(NoteListItemListQuery);
        }

        public Task CreateNoteCommandHandler(CreateNoteCommand command)
        {
            _taskQueue.Enqueue(_service.Create(command.Dto));
            return Task.CompletedTask;
        }

        public Task UpdateNoteCommandHandler(UpdateNoteCommand command)
        {
            _taskQueue.Enqueue(_service.Update(command.Dto));
            return Task.CompletedTask;
        }

        public Task DeleteNoteCommandHandler(DeleteNoteCommand command)
        {
            _taskQueue.Enqueue(_service.Delete(command.Dto));
            return Task.CompletedTask;
        }

        public Task<ICollection<NoteDto>> NoteListItemListQuery(NoteQuery query)
        {
            if (query.GetAll)
            {
                return _service.GetAll();
            }

            if (query.NotebookId != Guid.Empty)
            {
                return _service.FindByNotebookId(query.NotebookId);
            }

            return Task.FromResult((ICollection<NoteDto>)new List<NoteDto>());
        }

        private async void _updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _updateTimer.Stop();

            try
            {
                while (_taskQueue.Count() > 0)
                {
                    var nextInLine = _taskQueue.Dequeue();
                    await nextInLine;
                }
            }
            finally
            {
                _updateTimer.Start();
            }
        }
    }
}
