using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface INotesService
    {
        Task<ICollection<NoteDto>> GetAll();

        Task<ICollection<NoteDto>> FindByNotebookId(Guid id);

        Task<NoteDto> GetById(Guid id);

        Task<NoteDto> Create(NoteDto dto);

        Task<NoteDto> Update(NoteDto dto);

        Task Delete(NoteDto dto);
    }
}
