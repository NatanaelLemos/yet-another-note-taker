using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YetAnotherNoteTaker.Client.Common.Events
{
    public class EventBroker : IEventBroker
    {
        private readonly Dictionary<string, List<object>> _subscriptions = new Dictionary<string, List<object>>();

        private readonly Func<Type, Task<bool>> _userIsAuthenticatedFactory;

        public EventBroker(Func<Type, Task<bool>> userIsAuthenticatedFactory)
        {
            _userIsAuthenticatedFactory = userIsAuthenticatedFactory;
        }

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

        public async Task Notify<TEvent>(TEvent command)
        {
            var key = GetKey<TEvent>();
            if (!_subscriptions.TryGetValue(key, out var listOfCallbacks))
            {
                return;
            }

            if (!await _userIsAuthenticatedFactory(typeof(TEvent)))
            {
                return;
            }

            await Task.WhenAll(listOfCallbacks.Select(c =>
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
