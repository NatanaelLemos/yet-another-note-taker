using System;
using System.Collections.Generic;

namespace NoteTaker.Client.State
{
    public class EventBroker
    {
        private readonly Dictionary<string, List<object>> _callbacks = new Dictionary<string, List<object>>();

        public void Listen<TEvent>(Action<TEvent> callback)
        {
            var key = typeof(Action<TEvent>).Name;
            AddListener(key, callback);
        }

        public void Listen<TEvent, TResult>(Func<TEvent, TResult> callback)
        {
            var key = typeof(Func<TEvent, TResult>).Name;
            AddListener(key, callback);
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

        public TResult Query<TEvent, TResult>(TEvent query)
        {
            var key = typeof(Func<TEvent, TResult>).Name;

            foreach (var callback in _callbacks)
            {
                if (callback.Key != key)
                {
                    continue;
                }

                foreach (var listener in callback.Value)
                {
                    if (listener is Func<TEvent, TResult> func)
                    {
                        return func(query);
                    }
                }
            }

            return default;
        }

        public void Command<TEvent>(TEvent command)
        {
            var key = typeof(Action<TEvent>).Name;

            foreach (var callback in _callbacks)
            {
                if (callback.Key != key)
                {
                    continue;
                }

                    (callback.Value as Action<TEvent>)?.Invoke(command);
            }
        }
    }
}
