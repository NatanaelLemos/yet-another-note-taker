using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace NLemos.Xamarin.Common.Helpers
{
    public static class FileHelpers
    {
        public static ImageSource ReadAsImageSource(string embeddedResourceName)
        {
            return ImageSource.FromResource(embeddedResourceName, Assembly.GetCallingAssembly());
        }

        public static string ReadAsString(string embeddedResourceName)
        {
            var assembly = Assembly.GetCallingAssembly();

            using (var stream = assembly.GetManifestResourceStream(embeddedResourceName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
