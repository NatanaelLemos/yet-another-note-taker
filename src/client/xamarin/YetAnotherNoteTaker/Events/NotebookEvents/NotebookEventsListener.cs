using System;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;
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
            var notebooks = await _service.GetAll(UserState.UserEmail);
            await _eventBroker.Notify(new ListNotebooksResult(notebooks));
        }

        private async Task EditNotebookCommandHandler(EditNotebookCommand arg)
        {
            var task = string.IsNullOrWhiteSpace(arg.Key)
                ? _service.Create(UserState.UserEmail, new NotebookDto { Name = arg.Name })
                : _service.Update(UserState.UserEmail, new NotebookDto { Key= arg.Key, Name = arg.Name });

            var result = await task;
            await _eventBroker.Notify(new EditNotebookResult(result));
        }

        private async Task DeleteNotebookCommandHandler(DeleteNotebookCommand arg)
        {
            await _service.Delete(arg.Key);
            await _eventBroker.Notify(new ListNotebooksCommand());
        }
    }
}
