using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Services
{
    public interface INotesService
    {
        Task<List<NoteDto>> GetAll(Guid userId);
        Task<List<NoteDto>> GetByNotebookId(Guid userId, Guid notebookId);
        Task<NoteDto> Create(NoteDto noteDto);
        Task<NoteDto> Update(NoteDto noteDto);
        Task Delete(Guid noteId);
    }
}
