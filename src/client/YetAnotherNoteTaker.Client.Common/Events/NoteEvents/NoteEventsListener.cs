using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.Common.Dtos;
using System.Nxlx.Observer;

namespace YetAnotherNoteTaker.Client.Common.Events.NoteEvents
{
    public class NoteEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotesService _service;

        public NoteEventsListener(IEventBroker eventBroker, INotesService service)
        {
            _eventBroker = eventBroker ?? throw new ArgumentNullException(nameof(eventBroker));
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void Start()
        {
            _eventBroker.Subscribe<ListNotesCommand>(ListNotesCommandHandler);
            _eventBroker.Subscribe<EditNoteCommand>(EditNoteCommandHandler);
            _eventBroker.Subscribe<DeleteNoteCommand>(DeleteNoteCommandHandler);
        }

        private async Task ListNotesCommandHandler(ListNotesCommand arg)
        {
            if (string.IsNullOrEmpty(arg.NotebookKey))
            {
                var notes = await _service.GetAll();
                await _eventBroker.Notify(new ListNotesResult(notes));
            }
            else
            {
                var notes = await _service.GetByNotebookKey(arg.NotebookKey);
                await _eventBroker.Notify(new ListNotesResult(notes));
            }
        }

        private async Task EditNoteCommandHandler(EditNoteCommand arg)
        {
            if (string.IsNullOrEmpty(arg.NoteKey))
            {
                await _service.Create(
                    new NoteDto
                    {
                        NotebookKey = arg.NotebookKey,
                        Name = arg.Name,
                        Body = arg.Body
                    });
            }
            else
            {
                await _service.Update(
                    new NoteDto
                    {
                        Key = arg.NoteKey,
                        NotebookKey = arg.NotebookKey,
                        Name = arg.Name,
                        Body = arg.Body
                    });
            }

            await _eventBroker.Notify(new ListNotesCommand(arg.NotebookKey));
        }

        private async Task DeleteNoteCommandHandler(DeleteNoteCommand arg)
        {
            await _service.Delete(arg.NotebookKey, arg.NoteKey);
            await _eventBroker.Notify(new ListNotesCommand(arg.NotebookKey));
        }
    }
}
