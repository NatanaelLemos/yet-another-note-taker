using Xamarin.Forms;

namespace YetAnotherNoteTaker.State
{
    public static class PageNavigator
    {
        public static void AddListener(MasterDetailPage listener)
        {
            PageNavigatorImpl.Instance.AddListener(listener);
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
