using System.Threading.Tasks;
using Xamarin.Forms;

namespace YetAnotherNoteTaker.Blazor.State
{
    public interface IPageNavigator
    {
        void AddListener(MasterDetailPage listener);

        Task NavigateTo<TPage>(params object[] args)
            where TPage : ContentPage;

        void ClearHistory();

        Task Back();
    }
}
