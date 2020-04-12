using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.Events.NoteEvents
{
    public class CreateNoteCommand
    {
        public CreateNoteCommand(NoteDto noteDetail)
        {
            Dto = noteDetail;
        }

        public NoteDto Dto { get; }
    }
}
