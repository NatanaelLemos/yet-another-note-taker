using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using YetAnotherNoteTaker.Helpers;

namespace YetAnotherNoteTaker.Views
{
    public class QuillEditor
    {
        private WebView _container;
        private string _body;
        private bool _darkTheme;
        private string _lastContent;

        public QuillEditor(WebView container, double height = 500, string body = "", bool darkTheme = false)
        {
            _container = container;
            _body = string.IsNullOrEmpty(body)
                ? string.Empty
                : body.Replace("\\n", "<br />");
            _darkTheme = darkTheme;

            _container.Source = new HtmlWebViewSource { Html = Html };

            Height = height;
            _lastContent = body;
        }

        public double Height
        {
            get => _container.HeightRequest;
            set => _container.HeightRequest = value;
        }

        public async Task<string> GetContent()
        {
            if (_container != null)
            {
                try
                {
                    _lastContent = await _container.EvaluateJavaScriptAsync("document.querySelector('.ql-editor').innerHTML");

                    if (!string.IsNullOrEmpty(_lastContent))
                    {
                        _lastContent = DecodeEncodedNonAsciiCharacters(_lastContent);
                    }
                }
                catch (NullReferenceException)
                {
                    _container = null;
                }
            }

            return _lastContent;
        }

        private string Html
        {
            get
            {
                var resourceName = _darkTheme
                    ? "YetAnotherNoteTaker.Assets.quill_dark.html"
                    : "YetAnotherNoteTaker.Assets.quill_light.html";
                var themeFile = FileHelpers.ReadAsString(resourceName);
                return themeFile
                        .Replace("{{title}}", "Editor")
                        .Replace("{{body}}", _body);
            }
        }

        private static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());
        }
    }
}
