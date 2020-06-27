using System;
using Xamarin.Forms;

namespace NLemos.Xamarin.Common.Extensions
{
    public static class StackLayoutExtensions
    {
        public static void SetDynamicWidth(this ContentView contentView)
        {
            contentView.Padding = GetWidth(contentView);
            contentView.SizeChanged += Layout_SizeChanged;
        }

        public static void SetDynamicWidth(this StackLayout stackLayout)
        {
            stackLayout.Padding = GetWidth(stackLayout);
            stackLayout.SizeChanged += Layout_SizeChanged;
        }

        private static void Layout_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Layout layout)
            {
                layout.SizeChanged -= Layout_SizeChanged;
                layout.Padding = GetWidth(layout);
                layout.SizeChanged += Layout_SizeChanged;
            }
        }

        private static Thickness GetWidth(Layout layout)
        {
            var factor = 10;

            if (Application.Current.MainPage.Width > 1200)
            {
                factor = 20;
            }

            var padding = Application.Current.MainPage.Width / factor;
            var width = new Thickness(padding / 2, 10, padding / 2, layout.Padding.Bottom);
            return width;
        }
    }
}
