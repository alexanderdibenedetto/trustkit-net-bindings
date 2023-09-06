using DataTheorem.TrustKit.Net.iOS;
using Foundation;
using UIKit;

namespace TrustKit.Maui;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        new HttpMessageHandlerFactory().InitSharedInstanceWithConfiguration();
        return base.FinishedLaunching(application, launchOptions);
    }
}

