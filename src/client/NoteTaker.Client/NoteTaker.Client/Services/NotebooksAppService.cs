using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteTaker.Client.State;
using NoteTaker.Client.State.NotebookEvents;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;
using NoteTaker.Domain.Services;

namespace NoteTaker.Client.Services
{
    public class NotebooksAppService : INotebooksAppService
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotebooksService _notebooksService;
        private readonly INotesService _notesService;

        public NotebooksAppService(IEventBroker eventBroker, INotebooksService notebooksService, INotesService notesService)
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
            return _notebooksService.Create(command.Dto);
        }

        public Task UpdateNotebookCommandHandler(UpdateNotebookCommand command)
        {
            return _notebooksService.Update(command.Dto);
        }

        public async Task DeleteNotebookCommandHandler(DeleteNotebookCommand command)
        {
            await _notebooksService.Delete(command.Dto);

            var notes = await _notesService.FindByNotebookId(command.Dto.Id);

            await Task.WhenAll(
                notes.Select(n => _eventBroker.Command(new DeleteNoteCommand(n))));
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
