using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteTaker.Client.State
{
    public interface IEventBroker
    {
        void Listen<TEvent>(Func<TEvent, Task> callback);

        void Listen<TEvent, TResult>(Func<TEvent, Task<TResult>> callback);

        Task<TResult> Query<TEvent, TResult>(TEvent query);

        Task Command<TEvent>(TEvent command);
    }
}
