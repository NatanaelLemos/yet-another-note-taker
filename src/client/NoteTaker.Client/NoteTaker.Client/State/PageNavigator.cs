using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace NoteTaker.Client.State
{
    public static class PageNavigator
    {
        private static MasterDetailPage s_listener;
        private static LinkedList<(Type pageType, object[] args)> s_history = new LinkedList<(Type, object[])>();

        public static void AddListener(MasterDetailPage listener)
        {
            s_listener = listener;

            if (listener.Detail == null)
            {
                return;
            }

            s_history.Clear();

            if (listener.Detail is NavigationPage navListener)
            {
                s_history.AddLast((navListener.CurrentPage.GetType(), null));
            }
            else
            {
                s_history.AddLast((listener.Detail.GetType(), null));
            }
        }

        public static void NavigateTo<TPage>(params object[] args)
            where TPage : ContentPage
        {
            while (s_history.Count > 20)
            {
                s_history.RemoveFirst();
            }

            var pageType = typeof(TPage);
            NavigateTo(pageType, args);
            s_history.AddLast((pageType, args));
        }

        public static void ClearHistory()
        {
            s_history.Clear();
        }

        private static void NavigateTo(Type pageType, object[] args)
        {
            var pageInstance = (Page)Activator.CreateInstance(pageType, args);

            if (s_listener != null)
            {
                s_listener.Detail = new NavigationPage(pageInstance);
                s_listener.IsPresented = false;
            }
        }

        public static void Back()
        {
            if (s_history.Count > 1)
            {
                s_history.RemoveLast();
                var last = s_history.Last.Value;
                NavigateTo(last.pageType, last.args);
            }
        }
    }
}
