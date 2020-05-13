using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotebooksService : INotebooksService
    {
        private readonly INotebooksRepository _repository;

        public NotebooksService(INotebooksRepository repository)
        {
            _repository = repository;
        }

        public Task<List<NotebookDto>> GetAll(string email)
        {
            return _repository.GetAll(email);
        }

        public Task<NotebookDto> Get(string email, string notebookKey)
        {
            return _repository.Get(email, notebookKey);
        }

        public Task<NotebookDto> Create(string email, NotebookDto notebookDto)
        {
            return _repository.Create(email, notebookDto);
        }

        public Task<NotebookDto> Update(string email, NotebookDto notebookDto)
        {
            return _repository.Update(email, notebookDto);
        }

        public Task Delete(string email, string notebookKey)
        {
            return _repository.Delete(email, notebookKey);
        }
    }
}
