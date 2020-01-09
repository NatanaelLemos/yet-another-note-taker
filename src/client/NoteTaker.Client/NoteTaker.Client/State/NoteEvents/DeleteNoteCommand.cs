using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.State.NoteEvents
{
    public class DeleteNoteCommand
    {
        public DeleteNoteCommand(NoteDto noteDetail)
        {
            Dto = noteDetail;
        }

        public NoteDto Dto { get; }
    }
}
