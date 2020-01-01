using System;
using Xamarin.Forms;

namespace NoteTaker.Client.Extensions
{
    public static class StackLayoutExtensions
    {
        public static void SetDynamicWidth(this StackLayout stackLayout)
        {
            stackLayout.Padding = GetWidth(stackLayout);
            stackLayout.SizeChanged += StackLayout_SizeChanged;
        }

        private static void StackLayout_SizeChanged(object sender, EventArgs e)
        {
            if (sender is StackLayout stackLayout)
            {
                stackLayout.SizeChanged -= StackLayout_SizeChanged;
                stackLayout.Padding = GetWidth(stackLayout);
                stackLayout.SizeChanged += StackLayout_SizeChanged;
            }
        }

        private static Thickness GetWidth(StackLayout stackLayout)
        {
            var factor = 10;

            if (Application.Current.MainPage.Width > 1200)
            {
                factor = 20;
            }

            var padding = Application.Current.MainPage.Width / factor;
            var width = new Thickness(padding / 2, 10, padding / 2, stackLayout.Padding.Bottom);
            return width;
        }
    }
}
