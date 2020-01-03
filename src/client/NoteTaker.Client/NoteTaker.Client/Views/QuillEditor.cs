using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NoteTaker.Client.Views
{
    public class QuillEditor
    {
        private static readonly string _htmlStarting =
            "<!DOCTYPE html><html lang=\"en\">" +
            "<head>" +
            "   <title>{{title}}</title>" +
            "   <meta charset=\"utf-8\">" +
            "   <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">" +
            "   <meta name=\"language\" content=\"english\">" +
            "   <meta name=\"viewport\" content=\"width=device-width\">" +
            "   <link href=\"https://cdn.quilljs.com/1.3.6/quill.snow.css\" rel=\"stylesheet\">" +
            "   <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/KaTeX/0.7.1/katex.min.css\" />" +
            "   <link rel=\"stylesheet\" href=\"https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/styles/monokai-sublime.min.css\" />" +
            "   <style>#editor-container{height:{{height}}px}</style>" +
            "</head>" +
            "<body>" +
            "   <div id=\"standalone-container\">" +
            "   <div id=\"toolbar-container\"> " +
            "     <span class=\"ql-formats\"> " +
            "         <select class=\"ql-font\"></select> " +
            "         <select class=\"ql-size\"></select> " +
            "     </span>" +
            "     <span class=\"ql-formats\"> " +
            "         <button class=\"ql-bold\"></button> " +
            "         <button class=\"ql-italic\"></button> " +
            "         <button class=\"ql-underline\"></button> " +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <select class=\"ql-color\"></select> " +
            "         <select class=\"ql-background\"></select>" +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <button class=\"ql-script\" value=\"sub\"></button> " +
            "         <button class=\"ql-script\" value=\"super\"></button> " +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <button class=\"ql-header\" value=\"1\"></button> " +
            "         <button class=\"ql-header\" value=\"2\"></button> " +
            "         <button class=\"ql-blockquote\"></button> " +
            "         <button class=\"ql-code-block\"></button> " +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <button class=\"ql-list\" value=\"ordered\"></button> " +
            "         <button class=\"ql-list\" value=\"bullet\"></button> " +
            "         <button class=\"ql-indent\" value=\"-1\"></button> " +
            "         <button class=\"ql-indent\" value=\"+1\"></button> " +
            "         <button value=\"todo\" id=\"btnTodo\">" +
            "           <svg class=\"\" viewbox=\"0 0 18 18\">" +
            "               <line class=\"ql-stroke\" x1=\"9\" x2=\"15\" y1=\"4\" y2=\"4\"></line>" +
            "               <polyline class=\"ql-stroke\" points=\"3 4 4 5 6 3\"></polyline>" +
            "               <line class=\"ql-stroke\" x1=\"9\" x2=\"15\" y1=\"14\" y2=\"14\"></line>" +
            "               <polyline class=\"ql-stroke\" points=\"3 14 4 15 6 13\"></polyline>" +
            "               <line class=\"ql-stroke\" x1=\"9\" x2=\"15\" y1=\"9\" y2=\"9\"></line>" +
            "               <polyline class=\"ql-stroke\" points=\"3 9 4 10 6 8\"></polyline>" +
            "           </svg>" +
            "       </button>" +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <select class=\"ql-align\"></select> " +
            "     </span> " +
            "     <span class=\"ql-formats\"> " +
            "         <button class=\"ql-link\"></button> " +
            "         <button class=\"ql-image\"></button> " +
            "     </span> " +
            "   </div>" +
            "   <div id=\"editor-container\">";

        private static readonly string _htmlEnding =
            "   </div>" +
            "</div> " +
            "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/KaTeX/0.7.1/katex.min.js\"></script>" +
            "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/highlight.js/9.12.0/highlight.min.js\"></script>" +
            "<script src=\"https://cdn.quilljs.com/1.3.6/quill.min.js\"></script> " +
            "<script>var quill=new Quill('#editor-container',{modules:{formula:true,syntax:true,toolbar:'#toolbar-container'},placeholder:'...',theme:'snow'});" +
            " document.getElementById('btnTodo').onclick = () => { quill.insertText(quill.getLength() -1, '☐'); quill.setSelection(quill.getLength() -1, 0); } " +
            "</script> " +
            "</body>" +
            "</html>";

        private WebView _container;
        private string _title;
        private string _text;
        private double _height;
        private string _lastContent;

        public QuillEditor(WebView container, string title = "Editor", double height = 500, string text = "")
        {
            _container = container;
            _title = title;
            _text = text.Replace("\\n", "<br />");
            _height = height - 120;

            _container.Source = new HtmlWebViewSource { Html = Html };
            _container.HeightRequest = height;
            _lastContent = text;
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
                return _htmlStarting
                    .Replace("{{title}}", _title)
                    .Replace("{{height}}", _height.ToString("N0")) +
                    _text + _htmlEnding;
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
