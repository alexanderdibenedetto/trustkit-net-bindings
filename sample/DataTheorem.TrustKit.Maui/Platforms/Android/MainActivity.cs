using Android.App;
using Android.Content.PM;
using Android.OS;
using DataTheorem.TrustKit.Net.Android;

namespace TrustKit.Maui;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        new HttpMessageHandlerFactory().InitializeWithNetworkSecurityConfiguration(this);
        base.OnCreate(savedInstanceState);
    }
}

