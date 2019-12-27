using System;
using System.Collections.Generic;

namespace NoteTaker.Domain
{
    /// <summary>
    /// Holds the state of a note.
    /// </summary>
    public class Note
    {
        public Note()
        {
            Id = Guid.NewGuid();
            Available = true;
            HistoryPosition = -1;
            History = new List<string>();
        }

        public Guid Id { get; set; }

        public bool Available { get; set; }

        public string Name { get; set; }

        public int HistoryPosition { get; set; }

        public IList<string> History { get; set; }

        public string CurrentState
        {
            get
            {
                if (HistoryPosition == -1)
                {
                    return string.Empty;
                }

                return History[HistoryPosition];
            }
            set
            {
                HistoryPosition++;

                for (var i = HistoryPosition; i < History.Count; i++)
                {
                    History.RemoveAt(i);
                }

                History.Add(value);
            }
        }

        public void Undo()
        {
            if (HistoryPosition > -1)
            {
                HistoryPosition--;
            }
        }

        public void Redo()
        {
            if (HistoryPosition < (History.Count - 1))
            {
                HistoryPosition++;
            }
        }
    }
}
