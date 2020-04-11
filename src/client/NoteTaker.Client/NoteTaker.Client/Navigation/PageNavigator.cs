using Xamarin.Forms;

namespace NoteTaker.Client.Navigation
{
    public static class PageNavigator
    {
        public static void AddListener(MasterDetailPage listener)
        {
            PageNavigatorImpl.Instance.AddListener(listener);
        }

        public static void AddLoginPage<TPage>()
            where TPage : ContentPage
        {
            PageNavigatorImpl.Instance.AddLoginPage<TPage>();
        }

        public static void NavigateTo<TPage>(params object[] args)
            where TPage : ContentPage
        {
            PageNavigatorImpl.Instance.NavigateTo<TPage>(args);
        }

        public static void ClearHistory()
        {
            PageNavigatorImpl.Instance.ClearHistory();
        }

        public static void Back()
        {
            PageNavigatorImpl.Instance.Back();
        }
    }
}
