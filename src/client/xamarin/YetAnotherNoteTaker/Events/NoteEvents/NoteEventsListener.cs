using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.NoteEvents
{
    public class NoteEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotesService _service;

        public NoteEventsListener(IEventBroker eventBroker, INotesService service)
        {
            _eventBroker = eventBroker;
            _service = service;
        }

        public void Start()
        {
            _eventBroker.Subscribe<ListNotesCommand>(ListNotesCommandHandler);
            _eventBroker.Subscribe<EditNoteCommand>(EditNoteCommandHandler);
            _eventBroker.Subscribe<DeleteNoteCommand>(DeleteNoteCommandHandler);
        }

        private async Task ListNotesCommandHandler(ListNotesCommand arg)
        {
            if (arg.NotebookId == Guid.Empty)
            {
                var notes = await _service.GetAll(UserState.UserId);
                await _eventBroker.Notify(new ListNotesResult(notes));
            }
            else
            {
                var notes = await _service.GetByNotebookId(UserState.UserId, arg.NotebookId);
                await _eventBroker.Notify(new ListNotesResult(notes));
            }
        }

        private async Task EditNoteCommandHandler(EditNoteCommand arg)
        {
            if (arg.NoteId == Guid.Empty)
            {
                await _service.Create(
                    UserState.UserId,
                    new NoteDto
                    {
                        NotebookId = arg.NotebookId,
                        Name = arg.Name,
                        Body = arg.Body
                    });
            }
            else
            {
                await _service.Update(
                    UserState.UserId,
                    new NoteDto
                    {
                        Id = arg.NoteId,
                        Name = arg.Name,
                        Body = arg.Body
                    });
            }

            await _eventBroker.Notify(new ListNotesCommand(arg.NotebookId));
        }

        private async Task DeleteNoteCommandHandler(DeleteNoteCommand arg)
        {
            await _service.Delete(arg.NoteId);
            await _eventBroker.Notify(new ListNotesCommand(arg.NotebookId));
        }
    }
}
