using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Events
{
    public class EventBroker : IEventBroker
    {
        private readonly Dictionary<string, List<object>> _subscriptions = new Dictionary<string, List<object>>();

        public void Subscribe<TEvent>(Func<TEvent, Task> callback)
        {
            var key = GetKey<TEvent>();
            if (_subscriptions.TryGetValue(key, out var listOfCallbacks))
            {
                listOfCallbacks.Add(callback);
            }
            else
            {
                _subscriptions.Add(key, new List<object>
                {
                    callback
                });
            }
        }

        public Task Notify<TEvent>(TEvent command)
        {
            var key = GetKey<TEvent>();
            if (!_subscriptions.TryGetValue(key, out var listOfCallbacks))
            {
                return Task.CompletedTask;
            }

            if (!UserState.IsAuthenticated(typeof(TEvent)))
            {
                return Task.CompletedTask;
            }

            return Task.WhenAll(listOfCallbacks.Select(c =>
                ((Func<TEvent, Task>)c)(command)));
        }

        public void Dispose()
        {
            _subscriptions.Clear();
        }

        private string GetKey<TEvent>()
        {
            return typeof(TEvent).Name;
        }
    }
}
