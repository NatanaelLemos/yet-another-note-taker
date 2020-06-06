using System.Collections.Generic;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotesService
    {
        Task<List<NoteDto>> GetAll();

        Task<List<NoteDto>> GetByNotebookKey(string notebookKey);

        Task<NoteDto> Get(string notebookKey, string noteKey);

        Task<NoteDto> Create(NoteDto noteDto);

        Task<NoteDto> Update(NoteDto noteDto);

        Task Delete(string notebookKey, string noteKey);
    }
}
