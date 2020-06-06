using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Blazor.Data
{
    public class BlazorNotesRepository : INotesRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public BlazorNotesRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public async Task<List<NoteDto>> GetAll(string email, string token)
        {
            var url = _urlBuilder.Notes.GetAll(email);
            return await _restClient.Get<List<NoteDto>>(url, token);
        }

        public async Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey, string token)
        {
            var url = _urlBuilder.Notes.GetByNotebookKey(email, notebookKey);
            return await _restClient.Get<List<NoteDto>>(url, token);
        }

        public Task<NoteDto> Get(string email, string notebookKey, string noteKey, string token)
        {
            var url = _urlBuilder.Notes.Get(email, notebookKey, noteKey);
            return _restClient.Get<NoteDto>(url, token);
        }

        public async Task<NoteDto> Create(string email, NoteDto noteDto, string token)
        {
            var url = _urlBuilder.Notes.Post(email, noteDto.NotebookKey);
            return await _restClient.Post<NoteDto>(url, noteDto, token);
        }

        public async Task<NoteDto> Update(string email, NoteDto noteDto, string token)
        {
            var url = _urlBuilder.Notes.Put(email, noteDto.NotebookKey, noteDto.Key);
            return await _restClient.Put<NoteDto>(url, noteDto, token);
        }

        public async Task Delete(string email, string notebookKey, string noteKey, string token)
        {
            var url = _urlBuilder.Notes.Delete(email, notebookKey, noteKey);
            await _restClient.Delete(url, token);
        }
    }
}
