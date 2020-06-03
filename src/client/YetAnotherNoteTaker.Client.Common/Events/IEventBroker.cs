using System;
using System.Threading.Tasks;

namespace YetAnotherNoteTaker.Client.Common.Events
{
    public interface IEventBroker : IDisposable
    {
        void Subscribe<TEvent>(Func<TEvent, Task> callback);

        Task Notify<TEvent>(TEvent command);
    }
}
