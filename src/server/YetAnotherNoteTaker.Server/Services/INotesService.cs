using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Server.Services
{
    public interface INotesService
    {
        Task<List<NoteDto>> GetAll(string userEmail);

        Task<List<NoteDto>> GetByNotebookKey(string userEmail, string notebookKey);

        Task<NoteDto> Get(string userEmail, string notebookKey, string noteKey);

        Task<NoteDto> Add(string userEmail, string notebookKey, NoteDto dto);

        Task<NoteDto> Update(string userEmail, string notebookKey, string noteKey, NoteDto dto);

        Task Delete(string userEmail, string notebookKey, string noteKey);
    }
}
