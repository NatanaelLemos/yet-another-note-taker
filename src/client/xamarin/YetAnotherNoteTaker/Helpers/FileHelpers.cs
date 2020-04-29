using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace YetAnotherNoteTaker.Helpers
{
    public static class FileHelpers
    {
        public static ImageSource ReadAsImageSource(string embeddedResourceName)
        {
            return ImageSource.FromResource(embeddedResourceName, Assembly.GetExecutingAssembly());
        }

        public static string ReadAsString(string embeddedResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
