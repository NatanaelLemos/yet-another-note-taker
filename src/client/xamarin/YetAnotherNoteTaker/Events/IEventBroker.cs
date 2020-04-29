using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherNoteTaker.Events
{
    public interface IEventBroker : IDisposable
    {
        void Subscribe<TEvent>(Func<TEvent, Task> callback);

        Task Notify<TEvent>(TEvent command);

    }
}
