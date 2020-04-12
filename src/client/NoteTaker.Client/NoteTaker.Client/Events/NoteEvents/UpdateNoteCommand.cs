using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.NoteEvents
{
    public class UpdateNoteCommand
    {
        public UpdateNoteCommand(NoteDto noteDetail)
        {
            Dto = noteDetail;
        }

        public NoteDto Dto { get; }
    }
}
