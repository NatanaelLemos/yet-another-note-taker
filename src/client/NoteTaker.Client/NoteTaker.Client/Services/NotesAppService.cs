using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotesAppService : INotesAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotesService _service;

        public NotesAppService(IEventBroker eventBroker, INotesService service)
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
            return _service.Create(command.Dto);
        }

        public Task UpdateNoteCommandHandler(UpdateNoteCommand command)
        {
            return _service.Update(command.Dto);
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
    }
}
