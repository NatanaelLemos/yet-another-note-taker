using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Client.Events;
using NoteTaker.Client.Events.NotebookEvents;
using NoteTaker.Client.Events.NoteEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotebooksAppService : TimedAppServiceBase, INotebooksAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotebooksService _notebooksService;
        private readonly INotesService _notesService;

        public NotebooksAppService(IEventBroker eventBroker, INotebooksService notebooksService, INotesService notesService)
            : base(500)
        {
            _eventBroker = eventBroker;
            _notebooksService = notebooksService;
            _notesService = notesService;
        }

        public void StartListeners()
        {
            _eventBroker.Listen<CreateNotebookCommand>(CreateNotebookCommandHandler);
            _eventBroker.Listen<UpdateNotebookCommand>(UpdateNotebookCommandHandler);
            _eventBroker.Listen<DeleteNotebookCommand>(DeleteNotebookCommandHandler);

            _eventBroker.Listen<NotebookQuery, NotebookDto>(NotebookItemQueryHandler);
            _eventBroker.Listen<NotebookQuery, ICollection<NotebookDto>>(NotebookListQueryHandler);
        }

        public Task CreateNotebookCommandHandler(CreateNotebookCommand command)
        {
            Enqueue(_notebooksService.Create(command.Dto));
            return Task.CompletedTask;
        }

        public Task UpdateNotebookCommandHandler(UpdateNotebookCommand command)
        {
            Enqueue(_notebooksService.Update(command.Dto));
            return Task.CompletedTask;
        }

        public async Task DeleteNotebookCommandHandler(DeleteNotebookCommand command)
        {
            Enqueue(_notebooksService.Delete(command.Dto));

            var notes = await _notesService.FindByNotebookId(command.Dto.Id);
            foreach (var note in notes)
            {
                Enqueue(_eventBroker.Command(new DeleteNoteCommand(note)));
            }
        }

        public Task<NotebookDto> NotebookItemQueryHandler(NotebookQuery query)
        {
            return _notebooksService.GetById(query.NotebookId);
        }

        public Task<ICollection<NotebookDto>> NotebookListQueryHandler(NotebookQuery query)
        {
            if (query.GetAll)
            {
                return _notebooksService.GetAll();
            }
            else
            {
                return Task.FromResult((ICollection<NotebookDto>)new List<NotebookDto>());
            }
        }
    }
}
