using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker.Client.Views
{
    public class QuillEditor
    {
        private WebView _container;
        private string _title;
        private string _body;
        private bool _darkTheme;
        private string _lastContent;

        public QuillEditor(WebView container, string title = "Editor", double height = 500, string body = "", bool darkTheme = false)
        {
            _container = container;
            _title = title;
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
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = _darkTheme
                    ? "NoteTaker.Client.Assets.quill_dark.html"
                    : "NoteTaker.Client.Assets.quill_light.html";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result
                        .Replace("{{title}}", _title)
                        .Replace("{{body}}", _body);
                }
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
