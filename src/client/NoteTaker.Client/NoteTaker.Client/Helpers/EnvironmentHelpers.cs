using Xamarin.Forms;

namespace NoteTaker.Client.Helpers
{
    public static class EnvironmentHelpers
    {
        public static EnvironmentName EnvironmentName
        {
            get
            {
                try
                {
                    switch (Device.RuntimePlatform)
                    {
                        case Device.iOS: return EnvironmentName.Ios;
                        case Device.Android: return EnvironmentName.Android;
                        case Device.UWP: return EnvironmentName.UWP;
                        default: return EnvironmentName.Mac;
                    }
                }
                catch
                {
                    return EnvironmentName.Mac;
                }
            }
        }
    }

    public enum EnvironmentName
    {
        UWP,
        Android,
        Ios,
        Mac
    }
}
