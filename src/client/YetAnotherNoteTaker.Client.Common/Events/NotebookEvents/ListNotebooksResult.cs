using System.Collections.Generic;
using YetAnotherNoteTaker.Common.Dtos;

namespace YetAnotherNoteTaker.Client.Common.Events.NotebookEvents
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
