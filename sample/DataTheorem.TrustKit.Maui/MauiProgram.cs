using Microsoft.Extensions.Logging;
using DataTheorem.TrustKit.Net.Core;
#if IOS
using DataTheorem.TrustKit.Net.iOS;
#elif ANDROID
using DataTheorem.TrustKit.Net.Android;
#endif

namespace TrustKit.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IHttpMessageHandlerFactory, HttpMessageHandlerFactory>();
        return builder.Build();
	}
}