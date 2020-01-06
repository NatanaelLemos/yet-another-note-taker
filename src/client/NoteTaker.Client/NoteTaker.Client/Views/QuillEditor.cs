using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
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
        private double _height;
        private string _lastContent;

        public QuillEditor(WebView container, string title = "Editor", double height = 500, string body = "")
        {
            _container = container;
            _title = title;
            _body = string.IsNullOrEmpty(body)
                ? string.Empty
                : body.Replace("\\n", "<br />");

            _height = height - 120;

            _container.Source = new HtmlWebViewSource { Html = Html };
            _container.HeightRequest = height;
            _lastContent = body;
        }

        public async Task<string> GetContent()
        {
            if (_container != null)
            {
                try
                {
                    _lastContent = await _container.EvaluateJavaScriptAsync("document.querySelector('.ql-editor').innerHTML");
                    _lastContent = DecodeEncodedNonAsciiCharacters(_lastContent);
                }
                catch (NullReferenceException)
                {
                }
            }

            return _lastContent;
        }

        private string Html
        {
            get
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "NoteTaker.Client.Assets.quill.html";

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var reader = new StreamReader(stream))
                {
                    var result = reader.ReadToEnd();
                    return result
                        .Replace("{{title}}", _title)
                        .Replace("{{height}}", _height.ToString("N0"))
                        .Replace("{{body}}", _body);
                }
            }
        }

        private static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
    }
}
