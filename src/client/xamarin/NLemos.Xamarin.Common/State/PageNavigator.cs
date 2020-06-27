using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using YetAnotherNoteTaker.Blazor.State;

namespace NLemos.Xamarin.Common.State
{
    public class PageNavigator : IPageNavigator
    {
        private MasterDetailPage _listener;
        private readonly LinkedList<(Type pageType, object[] args)> _history = new LinkedList<(Type, object[])>();
        private readonly Func<Type, Task<bool>> _userIsAuthenticatedFactory;

        public PageNavigator(
            Func<Type, Task<bool>> userIsAuthenticatedFactory = null)
        {
            _userIsAuthenticatedFactory = userIsAuthenticatedFactory;
        }

        public void AddListener(MasterDetailPage listener)
        {
            _listener = listener;

            if (listener.Detail == null)
            {
                return;
            }

            _history.Clear();

            if (listener.Detail is NavigationPage navListener)
            {
                _history.AddLast((navListener.CurrentPage.GetType(), null));
            }
            else
            {
                _history.AddLast((listener.Detail.GetType(), null));
            }
        }

        public Task NavigateTo<TPage>(params object[] args) where TPage : ContentPage
        {
            var pageType = typeof(TPage);
            return NavigateTo(pageType, args);
        }

        public Task Back()
        {
            if (_history.Count > 1)
            {
                _history.RemoveLast();
                var last = _history.Last.Value;
                return NavigateTo(last.pageType, last.args);
            }
            return Task.CompletedTask;
        }

        public void ClearHistory()
        {
            _history.Clear();
        }

        private async Task NavigateTo(Type pageType, object[] args)
        {
            while (_history.Count > 20)
            {
                _history.RemoveFirst();
            }

            if (await _userIsAuthenticatedFactory(pageType))
            {
                var pageInstance = (Page)Activator.CreateInstance(pageType, args);

                if (_listener != null)
                {
                    _listener.Detail = new NavigationPage(pageInstance);

                    if (_listener.MasterBehavior == MasterBehavior.Popover)
                    {
                        _listener.IsPresented = false;
                    }
                }

                _history.AddLast((pageType, args));
            }
        }
    }
}
