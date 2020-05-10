using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.Http;
using YetAnotherNoteTaker.Common.Dtos;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Data
{
    public class XamarinNotesRepository : INotesRepository
    {
        private readonly IRestClient _restClient;
        private readonly IUrlBuilder _urlBuilder;

        public XamarinNotesRepository(IRestClient restClient, IUrlBuilder urlBuilder)
        {
            _restClient = restClient;
            _urlBuilder = urlBuilder;
        }

        public Task<List<NoteDto>> GetAll(string email)
        {
            var url = _urlBuilder.Notes.GetAll(email);
            return _restClient.Get<List<NoteDto>>(url, UserState.Token);
        }

        public Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey)
        {
            var url = _urlBuilder.Notes.GetByNotebookKey(email, notebookKey);
            return _restClient.Get<List<NoteDto>>(url, UserState.Token);
        }

        public Task<NoteDto> Create(string email, NoteDto noteDto)
        {
            var url = _urlBuilder.Notes.Post(email, noteDto.NotebookKey);
            return _restClient.Post<NoteDto>(url, noteDto, UserState.Token);
        }

        public Task<NoteDto> Update(string email, NoteDto noteDto)
        {
            var url = _urlBuilder.Notes.Put(email, noteDto.NotebookKey, noteDto.Key);
            return _restClient.Put<NoteDto>(url, noteDto, UserState.Token);
        }

        public Task Delete(string email, string notebookKey, string noteKey)
        {
            var url = _urlBuilder.Notes.Delete(email, notebookKey, noteKey);
            return _restClient.Delete(url, UserState.Token);
        }
    }
}
