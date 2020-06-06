using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;
        private readonly IUserState _userState;

        public NotebooksService(INotebooksRepository repository, IUserState userState)
        {
            _repository = repository;
            _userState = userState;
        }

        public async Task<List<NotebookDto>> GetAll()
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.GetAll(email, token);
        }

        public async Task<NotebookDto> Get(string notebookKey)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;

            if (string.IsNullOrWhiteSpace(notebookKey) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return await _repository.Get(email, notebookKey, token);
        }

        public async Task<NotebookDto> Create(NotebookDto notebookDto)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.Create(email, notebookDto, token);
        }

        public async Task<NotebookDto> Update(NotebookDto notebookDto)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.Update(email, notebookDto, token);
        }

        public async Task Delete(string notebookKey)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            await _repository.Delete(email, notebookKey, token);
        }
    }
}
