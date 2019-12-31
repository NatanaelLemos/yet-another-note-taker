using System;
using System.Collections.Generic;
using System.Text;
using NoteTaker.Domain.Dtos;

namespace NoteTaker.Client.State.NoteEvents
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
