using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Domain.Services
{
    public interface INotesService
    {
        Task<ICollection<NoteListItemDto>> GetAll();

        Task<ICollection<NoteListItemDto>> FindByNotebookId(Guid id);

        Task<NoteDetailDto> GetById(Guid id);

        Task<NoteDetailDto> CreateOrUpdate(NoteDetailDto noteDetailDto);
    }
}
