using System.Net.Http;
using Javax.Net.Ssl;
using Xamarin.Android.Net;
using Android.Content;
using TrustKit.Xamarin.Core;
using Android.Runtime;

namespace TrustKit.Xamarin.Android
{
    [Preserve(AllMembers = true)]
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public HttpMessageHandlerFactory() { }

        public static void Init(Context context)
        {
            Com.Datatheorem.Android.Trustkit.TrustKit.InitializeWithNetworkSecurityConfiguration(context);
        }

        /// <inheritdoc />
        public HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitAndroidClientHandler();
        }

        protected sealed class TrustKitAndroidClientHandler : AndroidClientHandler
        {
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return Com.Datatheorem.Android.Trustkit.TrustKit.Instance.GetSSLSocketFactory(connection.URL.Host);
            }
        }
    }
}
