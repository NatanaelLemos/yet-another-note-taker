using System.Collections.Generic;
using YetAnotherNoteTaker.Common.Dtos;

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
