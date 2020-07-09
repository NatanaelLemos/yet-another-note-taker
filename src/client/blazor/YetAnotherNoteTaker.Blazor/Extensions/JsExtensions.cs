using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace YetAnotherNoteTaker.Blazor.Extensions
{
    public static class JsExtensions
    {
        public static async Task FocusElement(this IJSRuntime js, string elementId)
        {
            await js.InvokeVoidAsync("focusElement", elementId);
        }

        public static async Task Alert(this IJSRuntime js, string message)
        {
            await js.InvokeVoidAsync("alert", message);
        }

        public static async Task<bool> Confirm(this IJSRuntime js, string message)
        {
            return await js.InvokeAsync<bool>("confirm", message);
        }

        public static async Task NavigateBack(this IJSRuntime js)
        {
            await js.InvokeVoidAsync("window.history.back");
        }
    }
}
