using Microsoft.Extensions.Logging;
using TrustKit.Net.Core;
#if IOS
using TrustKit.Net.iOS;
#elif ANDROID
using TrustKit.Net.Android;
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

#if IOS
		builder.Services.AddSingleton<IHttpClientHandlerFactory, HttpClientHandlerFactory>();
#elif ANDROID
        builder.Services.AddSingleton<IHttpClientHandlerFactory, HttpClientHandlerFactory>();
#endif
        return builder.Build();
	}
}

