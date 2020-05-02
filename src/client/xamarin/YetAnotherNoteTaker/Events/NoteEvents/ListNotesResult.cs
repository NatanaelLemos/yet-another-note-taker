using System.Collections.Generic;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Events.NoteEvents
{
    public class ListNotesResult
    {
        public ListNotesResult(List<NoteDto> notes)
        {
            Notes = notes;
        }

        public List<NoteDto> Notes { get; }
    }
}
