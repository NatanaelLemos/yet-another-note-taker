using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Data;
using YetAnotherNoteTaker.Client.Common.State;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public class NotesService : INotesService
    {
        private readonly INotesRepository _repository;
        private readonly IUserState _userState;

        public NotesService(INotesRepository repository, IUserState userState)
        {
            _repository = repository;
            _userState = userState;
        }

        public async Task<List<NoteDto>> GetAll()
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.GetAll(email, token);
        }

        public async Task<List<NoteDto>> GetByNotebookKey(string notebookKey)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.GetByNotebookKey(email, notebookKey, token);
        }

        public async Task<NoteDto> Create(NoteDto noteDto)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.Create(email, noteDto, token);
        }

        public async Task<NoteDto> Update(NoteDto noteDto)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            return await _repository.Update(email, noteDto, token);
        }

        public async Task Delete(string notebookKey, string noteKey)
        {
            var email = await _userState.UserEmail;
            var token = await _userState.Token;
            await _repository.Delete(email, notebookKey, noteKey, token);
        }
    }
}
