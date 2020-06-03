using AppKit;
using Foundation;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

namespace YetAnotherNoteTaker.Mac
{
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        private NSWindow _window;

        public AppDelegate()
        {
            var style = NSWindowStyle.Closable
                | NSWindowStyle.Resizable
                | NSWindowStyle.Titled;
            var rect = new CoreGraphics.CGRect(200, 200, 1024, 768);

            _window = new NSWindow(rect, style, NSBackingStore.Buffered, false);
            _window.Title = "NoteTaker";
            _window.TitleVisibility = NSWindowTitleVisibility.Hidden;
        }

        public override NSWindow MainWindow => _window;

        public override void DidFinishLaunching(NSNotification notification)
        {
            NSApplication.SharedApplication.SetAppearance(NSAppearance.GetAppearance(NSAppearance.NameDarkAqua));

            Forms.Init();
            LoadApplication(new YetAnotherNoteTaker.App());
            base.DidFinishLaunching(notification);
        }

        public override void WillTerminate(NSNotification notification)
        {
            // Insert code here to tear down your application
        }
    }
}