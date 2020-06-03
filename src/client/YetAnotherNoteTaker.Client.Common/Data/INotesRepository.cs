using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Data
{
    public interface INotesRepository
    {
        Task<List<NoteDto>> GetAll(string email, string token);
        Task<List<NoteDto>> GetByNotebookKey(string email, string notebookKey, string token);
        Task<NoteDto> Create(string email, NoteDto noteDto, string token);
        Task<NoteDto> Update(string email, NoteDto noteDto, string token);
        Task Delete(string email, string notebookKey, string noteKey, string token);
    }
}
