using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xamarin.Forms.StyleSheets;
using YetAnotherNoteTaker.Helpers;
using YetAnotherNoteTaker.State;

namespace YetAnotherNoteTaker.Views
{
    public static class Themes
    {
        private const string TemplatePath = "YetAnotherNoteTaker.Assets.style_template.css";

        public static void ApplyLightTheme(App app)
        {
            if (EnvironmentHelpers.EnvironmentName != EnvironmentName.Mac)
            {
                ApplyTheme(app, new LightTheme());
            }

            PageNavigator.ClearHistory();
            app.MainPage = new MainPage();
        }

        public static void ApplyDarkTheme(App app)
        {
            if (EnvironmentHelpers.EnvironmentName != EnvironmentName.Mac)
            {
                ApplyTheme(app, new DarkTheme());
            }

            PageNavigator.ClearHistory();
            app.MainPage = new MainPage();
        }

        private static void ApplyTheme(App app, ThemeStyle style)
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

            var template = FileHelpers.ReadAsString(TemplatePath);
            template = style.ReplaceVariables(template);
            app.Resources.Add(StyleSheet.FromString(template));
        }

        private interface ThemeStyle
        {
            string ReplaceVariables(string template);
        }

        private class LightTheme : ThemeStyle
        {
            public string ReplaceVariables(string template)
            {
                return template
                    .Replace("{{background-color}}", "#f3f3f8")
                    .Replace("{{accent-color}}", "#d7d7e3")
                    .Replace("{{color}}", "#333")
                    .Replace("{{placeholder-color}}", "#80808a");
            }
        }

        private class DarkTheme : ThemeStyle
        {
            public string ReplaceVariables(string template)
            {
                return template
                    .Replace("{{background-color}}", "#404052")
                    .Replace("{{accent-color}}", "#303042")
                    .Replace("{{color}}", "#f3f3f3")
                    .Replace("{{placeholder-color}}", "#838393");
            }
        }
    }
}
