using System.Diagnostics;
using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;
using DataTheorem.TrustKit.Net.Core;

namespace TrustKit.Maui
{
	public class MainPageModel : ObservableObject, IDisposable
    {
        private HttpClient _httpClient;
        private string text = "Tap above to change text.";
        private bool disposedValue;

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
                        string allowedText = await _httpClient.GetStringAsync(@"https://www.datatheorem.com");
                        Text = $"Domain pinned and connection allowed. Characters of text loaded: {allowedText.Length}";
                        break;

                    // ShouldBlockConnection
                    case "1":
                        string blockedText = await _httpClient.GetStringAsync(@"https://www.google.com");
                        Text = $"Domain should be blocked. You should not see this if properly configured. Characters of text loaded: {blockedText.Length}";
                        break;

                    // DomainNotPinned
                    case "2":
                        string notPinnedText = await _httpClient.GetStringAsync(@"https://www.microsoft.com/en-us/");
                        Text = $"Domain not pinned and successfully loaded.  Characters of text loaded: {notPinnedText.Length}";
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Text = $"Certificate Pinning Issue.{Environment.NewLine}{ex.Message ?? string.Empty}{Environment.NewLine}{ex.StackTrace}";
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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _httpClient?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}