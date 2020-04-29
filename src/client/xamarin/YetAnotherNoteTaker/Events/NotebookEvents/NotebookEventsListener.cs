using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;
using YetAnotherNoteTaker.Client.Common.Services;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class NotebookEventsListener
    {
        private readonly IEventBroker _eventBroker;
        private readonly INotebooksService _service;

        public NotebookEventsListener(IEventBroker eventBroker, INotebooksService service)
        {
            _eventBroker = eventBroker;
            _service = service;
        }

        public void Start()
        {
            _eventBroker.Subscribe<ListNotebooksCommand>(ListNotebooksCommandHandler);
            _eventBroker.Subscribe<EditNotebookCommand>(EditNotebookCommandHandler);
            _eventBroker.Subscribe<DeleteNotebookCommand>(DeleteNotebookCommandHandler);
        }

        private async Task ListNotebooksCommandHandler(ListNotebooksCommand arg)
        {
            var notebooks = await _service.GetAll(UserState.UserId);
            await _eventBroker.Notify(new ListNotebooksResult(notebooks));

        }

        private async Task EditNotebookCommandHandler(EditNotebookCommand arg)
        {
            var task = arg.Id == Guid.Empty
                ? _service.Create(new NotebookDto { Name = arg.Name })
                : _service.Update(new NotebookDto { Id = arg.Id, Name = arg.Name });

            var result = await task;
            await _eventBroker.Notify(new EditNotebookResult(result));
        }

        private async Task DeleteNotebookCommandHandler(DeleteNotebookCommand arg)
        {
            await _service.Delete(arg.Id);
            await _eventBroker.Notify(new ListNotebooksCommand());
        }
    }
}
