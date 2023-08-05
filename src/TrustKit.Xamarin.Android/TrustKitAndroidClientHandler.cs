using Android.Runtime;
using Javax.Net.Ssl;
using Xamarin.Android.Net;

namespace TrustKit.Xamarin.Android
{
    /// <summary>
    ///     The TrustKit Android Client Handler.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TrustKitAndroidClientHandler : AndroidClientHandler
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="TrustKitAndroidClientHandler"/>.
        /// </summary>
        public TrustKitAndroidClientHandler() { }

        /// <inheritdoc />
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return Com.Datatheorem.Android.Trustkit.TrustKit.Instance.GetSSLSocketFactory(connection.URL.Host);
        }
    }
}