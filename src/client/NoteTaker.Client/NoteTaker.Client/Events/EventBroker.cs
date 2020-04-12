using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoteTaker.Client.State;

namespace NoteTaker.Client.Events
{
    public class EventBroker : IEventBroker
    {
        private bool _isDisposed = false;
        private readonly Dictionary<string, List<object>> _callbacks = new Dictionary<string, List<object>>();

        public void Listen<TEvent>(Func<TEvent, Task> callback)
        {
            if (_isDisposed)
            {
                return;
            }

            var key = GetKey<TEvent>();
            AddListener(key, callback);
        }

        public void Listen<TEvent, TResult>(Func<TEvent, Task<TResult>> callback)
        {
            if (_isDisposed)
            {
                return;
            }

            var key = GetKey<TEvent, TResult>();
            AddListener(key, callback);
        }

        public Task<TResult> Query<TEvent, TResult>(TEvent query)
        {
            if (_isDisposed)
            {
                return Task.FromResult(default(TResult));
            }

            if (!UserState.IsAuthenticated(typeof(TEvent)))
            {
                return Task.FromResult(default(TResult));
            }

            var key = GetKey<TEvent, TResult>();

            if (!_callbacks.TryGetValue(key, out var listeners))
            {
                return Task.FromResult(default(TResult));
            }

            foreach (var listener in listeners)
            {
                if (listener is Func<TEvent, Task<TResult>> func)
                {
                    return func(query);
                }
            }

            return default;
        }

        public Task Command<TEvent>(TEvent command)
        {
            if (_isDisposed)
            {
                return Task.CompletedTask;
            }

            if (!UserState.IsAuthenticated(typeof(TEvent)))
            {
                return Task.CompletedTask;
            }

            var key = GetKey<TEvent>();
            var tasks = new List<Task>();

            if (!_callbacks.TryGetValue(key, out var listeners))
            {
                return Task.CompletedTask;
            }

            foreach (var listener in listeners)
            {
                if (listener is Func<TEvent, Task> task)
                {
                    tasks.Add(task(command));
                }
            }

            if (tasks.Any())
            {
                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _isDisposed = true;
            lock (_callbacks)
            {
                _callbacks.Clear();
            }
        }

        private string GetKey<TEvent>()
        {
            return typeof(TEvent).Name;
        }

        private string GetKey<TEvent, TResult>()
        {
            return $"{typeof(TEvent).Name}{typeof(TResult).Name}";
        }

        private void AddListener(string key, object listener)
        {
            List<object> value;

            if (_callbacks.ContainsKey(key))
            {
                value = _callbacks[key];
            }
            else
            {
                value = new List<object>();
                _callbacks.Add(key, value);
            }

            value.Add(listener);
        }
    }
}
