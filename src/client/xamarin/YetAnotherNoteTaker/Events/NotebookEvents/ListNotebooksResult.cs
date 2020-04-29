using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherNoteTaker.Client.Common.Dtos;

namespace YetAnotherNoteTaker.Events.NotebookEvents
{
    public class ListNotebooksResult
    {
        public ListNotebooksResult(List<NotebookDto> notebooks)
        {
            Notebooks = notebooks;
        }

        public List<NotebookDto> Notebooks { get; }
    }
}
