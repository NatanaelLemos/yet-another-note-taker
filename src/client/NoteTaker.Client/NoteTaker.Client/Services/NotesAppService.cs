using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotesAppService : TimedAppServiceBase, INotesAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotesService _service;

        public NotesAppService(IEventBroker eventBroker, INotesService service)
            : base(500)
        {
            _eventBroker = eventBroker;
            _service = service;
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
            Enqueue(_service.Create(command.Dto));
            return Task.CompletedTask;
        }

        public Task UpdateNoteCommandHandler(UpdateNoteCommand command)
        {
            Enqueue(_service.Update(command.Dto));
            return Task.CompletedTask;
        }

        public Task DeleteNoteCommandHandler(DeleteNoteCommand command)
        {
            Enqueue(_service.Delete(command.Dto));
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
    }
}
