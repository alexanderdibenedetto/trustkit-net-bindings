using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;
using DataTheorem.TrustKit.Net.Core;

namespace TrustKit.Maui
{
	public class MainPageModel : ObservableObject
    {
        private HttpClient _httpClient;
        private string text = "Tap above to change text.";

        public MainPageModel()
		{
            GetContentCommand = new Command(async (x) => await GetContentCommandExecute((string)x));
            SetupHttpClient();
        }

        private void SetupHttpClient()
        {
            IServiceProvider serviceProvider = Application.Current.Handler.MauiContext.Services;
            IHttpMessageHandlerFactory httpMessageHandlerFactory = serviceProvider.GetService<IHttpMessageHandlerFactory>();
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
                Debug.WriteLine(ex.Message);
                Text = $"Certificate Pinning Issue.{Environment.NewLine}{ex.Message ?? string.Empty}";
            }
        }

        public Command GetContentCommand { get; set; }

        public string Text { get => text;
            set
            {
                text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
    }
}