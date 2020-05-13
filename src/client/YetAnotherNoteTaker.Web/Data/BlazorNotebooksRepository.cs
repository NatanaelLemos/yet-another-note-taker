using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Web.State;

namespace YetAnotherNoteTaker.Web.Data
{
    public class BlazorNotebooksRepository : INotebooksRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IUserState _userState;

        public BlazorNotebooksRepository(IRestClient restClient, IUrlBuilder urlBuilder, IUserState userState)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
            _userState = userState;
        }

        public async Task<List<NotebookDto>> GetAll(string email)
        {
            var url = _urlBuilder.Notebooks.GetAll(email);
            var token = await _userState.GetToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                return new List<NotebookDto>();
            }

            var result = await _restClient.Get<List<NotebookDto>>(url, token);
            return result;
        }

        public async Task<NotebookDto> Get(string email, string notebookKey)
        {
            var url = _urlBuilder.Notebooks.Get(email, notebookKey);
            var token = await _userState.GetToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            return await _restClient.Get<NotebookDto>(url, token);
        }

        public async Task<NotebookDto> Create(string email, NotebookDto notebookDto)
        {
            var url = _urlBuilder.Notebooks.Post(email);
            return await _restClient.Post<NotebookDto>(url, notebookDto, await _userState.GetToken());
        }

        public async Task<NotebookDto> Update(string email, NotebookDto notebookDto)
        {
            var url = _urlBuilder.Notebooks.Put(email, notebookDto.Key);
            return await _restClient.Put<NotebookDto>(url, notebookDto, await _userState.GetToken());
        }

        public async Task Delete(string email, string notebookKey)
        {
            var url = _urlBuilder.Notebooks.Delete(email, notebookKey);
            await _restClient.Delete(url, await _userState.GetToken());
        }
    }

}
