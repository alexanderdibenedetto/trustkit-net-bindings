using Android.Util;
using System.Net.Http.Headers;
using TrustKit.Net.Core;

namespace TrustKit.Maui;

public partial class MainPage : ContentPage
{
    HttpClient _httpClient;

    public MainPage()
    {
        InitializeComponent();
        BindingContext = this;
        GetContentCommand = new Command(async (x) => await GetContentCommandExecute((string)x));
        SetupHttpClient();
    }

    private void SetupHttpClient()
    {
        IHttpMessageHandlerFactory httpMessageHandlerFactory = DependencyService.Resolve<IHttpMessageHandlerFactory>();
        _httpClient = new HttpClient(httpMessageHandlerFactory.BuildHttpMessageHandler());
        _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
    }

    private async Task GetContentCommandExecute(string value)
    {
        try
        {
            switch (value)
            {
                // ShouldAllowConnection
                case "0":
                    Text = await _httpClient.GetStringAsync(@"https://datatheorem.com");
                    break;

                // ShouldBlockConnection
                case "1":
                    Text = await _httpClient.GetStringAsync(@"https://www.google.com");
                    break;

                // DomainNotPinned
                case "2":
                    Text = await _httpClient.GetStringAsync(@"https://www.microsoft.com");
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Error("", ex.Message);
            Text = $"Certificate Pinning Issue.{Environment.NewLine}{ex.Message ?? string.Empty}";
        }
    }

    public Command GetContentCommand { get; set; }

    public string Text { get; set; }
}