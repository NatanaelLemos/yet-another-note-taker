using System.Collections.Generic;
using System.Threading.Tasks;
using NoteTaker.Client.State.NoteEvents;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Services
{
    public interface INotesAppService
    {
        void StartListeners();

        Task CreateNoteCommandHandler(CreateNoteCommand command);

        Task UpdateNoteCommandHandler(UpdateNoteCommand command);

        Task DeleteNoteCommandHandler(DeleteNoteCommand command);

        Task<ICollection<NoteDto>> NoteListItemListQuery(NoteQuery query);
    }
}
