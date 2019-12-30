using AppKit;

namespace NoteTaker.Client.Mac
{
    internal static class MainClass
    {
        private static void Main(string[] args)
        {
            NSApplication.Init();
            NSApplication.SharedApplication.Delegate = new AppDelegate();
            NSApplication.Main(args);
        }
    }
}