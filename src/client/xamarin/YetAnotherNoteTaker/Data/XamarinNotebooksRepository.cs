using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.State;

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

        public Task<List<NotebookDto>> GetAll(string email)
        {
            var url = _urlBuilder.Notebooks.GetAll(email);
            return _restClient.Get<List<NotebookDto>>(url, UserState.Token);
        }

        public Task<NotebookDto> Create(string email, NotebookDto notebookDto)
        {
            var url = _urlBuilder.Notebooks.Post(email);
            return _restClient.Post<NotebookDto>(url, notebookDto, UserState.Token);
        }

        public Task<NotebookDto> Update(string email, NotebookDto notebookDto)
        {
            var url = _urlBuilder.Notebooks.Put(email, notebookDto.Key);
            return _restClient.Put<NotebookDto>(url, notebookDto, UserState.Token);
        }

        public Task Delete(string email, string notebookKey)
        {
            var url = _urlBuilder.Notebooks.Delete(email, notebookKey);
            return _restClient.Delete(url, UserState.Token);
        }
    }
}
