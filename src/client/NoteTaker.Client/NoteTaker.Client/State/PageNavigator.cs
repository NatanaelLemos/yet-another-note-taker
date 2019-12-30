using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker.Client.State
{
    public class PageNavigator
    {
        private static readonly Lazy<PageNavigator> s_instance = new Lazy<PageNavigator>(() => new PageNavigator());

        private static PageNavigator Instance => s_instance.Value;

        private MasterDetailPage listener;
        private LinkedList<(Type pageType, object[] args)> history = new LinkedList<(Type, object[])>();

        private PageNavigator()
        {
        }

        public static void AddListener(MasterDetailPage listener)
        {
            Instance.listener = listener;

            if (listener.Detail == null)
            {
                return;
            }

            Instance.history.Clear();

            if (listener.Detail is NavigationPage navListener)
            {
                Instance.history.AddLast((navListener.CurrentPage.GetType(), null));
            }
            else
            {
                Instance.history.AddLast((listener.Detail.GetType(), null));
            }
        }

        public static void NavigateTo<TPage>(params object[] args)
            where TPage : ContentPage
        {
            var pageType = typeof(TPage);
            NavigateTo(pageType, args);
        }

        public static void NavigateTo(Type pageType, object[] args)
        {
            var pageInstance = (Page)Activator.CreateInstance(pageType, args);

            if (Instance.listener != null)
            {
                Instance.listener.Detail = new NavigationPage(pageInstance);
                Instance.listener.IsPresented = false;
            }

            Instance.history.AddLast((pageType, args));
        }

        public static void Back()
        {
            if (Instance.history.Count > 1)
            {
                Instance.history.RemoveLast();
                NavigateTo(Instance.history.Last.Value.pageType, Instance.history.Last.Value.args);
            }
        }
    }
}
