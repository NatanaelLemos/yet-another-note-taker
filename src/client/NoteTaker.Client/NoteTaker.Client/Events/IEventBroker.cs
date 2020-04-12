using System;
using System.Threading.Tasks;

namespace NoteTaker.Client.Events
{
    public interface IEventBroker : IDisposable
    {
        void Listen<TEvent>(Func<TEvent, Task> callback);

        void Listen<TEvent, TResult>(Func<TEvent, Task<TResult>> callback);

        Task<TResult> Query<TEvent, TResult>(TEvent query);

        Task Command<TEvent>(TEvent command);
    }
}
