using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.Web.State;

namespace YetAnotherNoteTaker.Web.Data
{
    public class BlazorNotesRepository : INotesRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;
        private readonly IUserState _state;

        public BlazorNotesRepository(IRestClient restClient, IUrlBuilder urlBuilder, IUserState state)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
            _state = state;
        }

        public async Task<List<NoteDto>> GetAll(string email)
        {
            var url = _urlBuilder.Notes.GetAll(email);
            return await _restClient.Get<List<NoteDto>>(url, await _state.GetToken());
        }

        public async Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey)
        {
            var url = _urlBuilder.Notes.GetByNotebookKey(email, notebookKey);
            return await _restClient.Get<List<NoteDto>>(url, await _state.GetToken());
        }

        public async Task<NoteDto> Create(string email, NoteDto noteDto)
        {
            var url = _urlBuilder.Notes.Post(email, noteDto.NotebookKey);
            return await _restClient.Post<NoteDto>(url, noteDto, await _state.GetToken());
        }

        public async Task<NoteDto> Update(string email, NoteDto noteDto)
        {
            var url = _urlBuilder.Notes.Put(email, noteDto.NotebookKey, noteDto.Key);
            return await _restClient.Put<NoteDto>(url, noteDto, await _state.GetToken());
        }

        public async Task Delete(string email, string notebookKey, string noteKey)
        {
            var url = _urlBuilder.Notes.Delete(email, notebookKey, noteKey);
            await _restClient.Delete(url, await _state.GetToken());
        }
    }
}
