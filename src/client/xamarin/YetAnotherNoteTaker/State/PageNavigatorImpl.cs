using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using YetAnotherNoteTaker.Client.Common.State;

namespace YetAnotherNoteTaker.State
{
    public class PageNavigatorImpl
    {
        private static readonly Lazy<PageNavigatorImpl> _instance = new Lazy<PageNavigatorImpl>(() => new PageNavigatorImpl());

        private MasterDetailPage _listener;
        private readonly LinkedList<(Type pageType, object[] args)> _history = new LinkedList<(Type, object[])>();

        private readonly IUserState _userState;

        private PageNavigatorImpl()
        {
            _userState = ServiceLocator.Get<IUserState>();
        }

        public static PageNavigatorImpl Instance => _instance.Value;

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

        public Task NavigateTo<TPage>(params object[] args)
            where TPage : ContentPage
        {
            var pageType = typeof(TPage);
            return NavigateTo(pageType, args);
        }

        public void ClearHistory()
        {
            _history.Clear();
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

        private async Task NavigateTo(Type pageType, object[] args)
        {
            while (_history.Count > 20)
            {
                _history.RemoveFirst();
            }

            if (await _userState.IsAuthenticated(pageType))
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
