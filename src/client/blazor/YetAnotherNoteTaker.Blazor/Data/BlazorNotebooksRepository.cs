using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Blazor.State;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Blazor.Data
{
    public class BlazorNotebooksRepository : INotebooksRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public BlazorNotebooksRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public async Task<List<NotebookDto>> GetAll(string email, string token)
        {
            var url = _urlBuilder.Notebooks.GetAll(email);

            if (string.IsNullOrWhiteSpace(token))
            {
                return new List<NotebookDto>();
            }

            var result = await _restClient.Get<List<NotebookDto>>(url, token);
            return result;
        }

        public async Task<NotebookDto> Get(string email, string notebookKey, string token)
        {
            var url = _urlBuilder.Notebooks.Get(email, notebookKey);

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return await _restClient.Get<NotebookDto>(url, token);
        }

        public async Task<NotebookDto> Create(string email, NotebookDto notebookDto, string token)
        {
            var url = _urlBuilder.Notebooks.Post(email);
            return await _restClient.Post<NotebookDto>(url, notebookDto, token);
        }

        public async Task<NotebookDto> Update(string email, NotebookDto notebookDto, string token)
        {
            var url = _urlBuilder.Notebooks.Put(email, notebookDto.Key);
            return await _restClient.Put<NotebookDto>(url, notebookDto, token);
        }

        public async Task Delete(string email, string notebookKey, string token)
        {
            var url = _urlBuilder.Notebooks.Delete(email, notebookKey);
            await _restClient.Delete(url, token);
        }
    }

}
