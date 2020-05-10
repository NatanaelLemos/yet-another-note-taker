using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotesService
    {
        Task<List<NoteDto>> GetAll(string email);

        Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey);

        Task<NoteDto> Create(string email, NoteDto noteDto);

        Task<NoteDto> Update(string email, NoteDto noteDto);

        Task Delete(string email, string notebookKey, string noteKey);
    }
}
