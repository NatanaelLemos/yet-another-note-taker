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

        private readonly Queue<UpdateNoteCommand> _updateQueue;
        private readonly Timer _updateTimer;

        public NotesAppService(IEventBroker eventBroker, INotesService service)
        {
            _eventBroker = eventBroker;
            _service = service;
            _updateQueue = new Queue<UpdateNoteCommand>();

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
            return _service.Create(command.Dto);
        }

        public Task UpdateNoteCommandHandler(UpdateNoteCommand command)
        {
            _updateQueue.Enqueue(command);
            return Task.CompletedTask;
        }

        public Task DeleteNoteCommandHandler(DeleteNoteCommand command)
        {
            return _service.Delete(command.Dto);
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
                while (_updateQueue.Count() > 0)
                {
                    var nextInLine = _updateQueue.Dequeue();
                    await _service.Update(nextInLine.Dto);
                }
            }
            finally
            {
                _updateTimer.Start();
            }
        }
    }
}
