using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NoteTaker.Client.Helpers;
using NoteTaker.Client.State;
using Xamarin.Forms.StyleSheets;

namespace NoteTaker.Client.Views
{
    public class Themes
    {
        public static void SetLightTheme(App app)
        {
            if (EnvironmentHelpers.EnvironmentName == EnvironmentName.Mac)
            {
                return;
            }

            SetTheme(app, "NoteTaker.Client.Assets.styles_light.css");
        }

        public static void SetDarkTheme(App app)
        {
            if (EnvironmentHelpers.EnvironmentName == EnvironmentName.Mac)
            {
                return;
            }

            SetTheme(app, "NoteTaker.Client.Assets.styles_dark.css");
        }

        private static void SetTheme(App app, string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var props = app.Resources.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
                var stylesheetsProp = props.FirstOrDefault(s => s.Name.ToLower().Equals("stylesheets"));
                if (stylesheetsProp != null)
                {
                    var stylesheets = stylesheetsProp.GetValue(app.Resources);
                    if (stylesheets != null)
                    {
                        ((IList<StyleSheet>)stylesheets).Clear();
                    }
                }
                app.Resources.Add(StyleSheet.FromReader(reader));
            }

            PageNavigator.ClearHistory();
            app.MainPage = new MainPage();
        }
    }
}
