using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Data
{
    public class XamarinNotebooksRepository : INotebooksRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public XamarinNotebooksRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public Task<List<NotebookDto>> GetAll(string email, string token)
        {
            var url = _urlBuilder.Notebooks.GetAll(email);
            return _restClient.Get<List<NotebookDto>>(url, token);
        }

        public Task<NotebookDto> Get(string email, string notebookKey, string token)
        {
            var url = _urlBuilder.Notebooks.Get(email, notebookKey);
            return _restClient.Get<NotebookDto>(url, token);
        }

        public Task<NotebookDto> Create(string email, NotebookDto notebookDto, string token)
        {
            var url = _urlBuilder.Notebooks.Post(email);
            return _restClient.Post<NotebookDto>(url, notebookDto, token);
        }

        public Task<NotebookDto> Update(string email, NotebookDto notebookDto, string token)
        {
            var url = _urlBuilder.Notebooks.Put(email, notebookDto.Key);
            return _restClient.Put<NotebookDto>(url, notebookDto, token);
        }

        public Task Delete(string email, string notebookKey, string token)
        {
            var url = _urlBuilder.Notebooks.Delete(email, notebookKey);
            return _restClient.Delete(url, token);
        }
    }
}
