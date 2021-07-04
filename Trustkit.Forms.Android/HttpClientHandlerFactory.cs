using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Java.Security;
using Javax.Net.Ssl;
using Trustkit.Forms.Interfaces;
using Xamarin.Android.Net;
using Xamarin.Forms;
using Com.Datatheorem.Android.Trustkit;

[assembly: Dependency(typeof(Trustkit.Forms.Droid.HttpMessageHandlerFactory))]
namespace Trustkit.Forms.Droid
{
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitAndroidClientHandler();
        }

        protected class TrustKitAndroidClientHandler : AndroidClientHandler
        {
            protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
            {
                return TrustKit.Instance.GetSSLSocketFactory(connection.URL.Host);
            }
        }
    }
}
