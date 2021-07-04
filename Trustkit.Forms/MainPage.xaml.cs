using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Serilog;
using Trustkit.Forms.Interfaces;
using Xamarin.Forms;

namespace Trustkit.Forms
{
    public partial class MainPage : ContentPage
    {
        HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            GetContentCommand = new Command(async () => await GetContentCommandExecute());
            SetupHttpClient();
        }

        private void SetupHttpClient()
        {
            IHttpMessageHandlerFactory httpMessageHandlerFactory = DependencyService.Resolve<IHttpMessageHandlerFactory>();
            _httpClient = new HttpClient(httpMessageHandlerFactory.BuildHttpMessageHandler());
            _httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        }

        private async Task GetContentCommandExecute()
        {
            try
            {
                Text = await _httpClient.GetStringAsync("https://datatheorem.com");
                //Text = await _httpClient.GetStringAsync("https://www.google.com");
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                Text = ex.Message ?? string.Empty;
            }
        }

        public Command GetContentCommand { get; set; }

        public string Text { get; set; }
    }
}
