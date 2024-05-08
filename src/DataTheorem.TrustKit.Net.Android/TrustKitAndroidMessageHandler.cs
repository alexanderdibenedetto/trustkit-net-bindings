using Android.Runtime;
using Javax.Net.Ssl;
using Xamarin.Android.Net;

namespace DataTheorem.TrustKit.Net.Android
{
    /// <summary>
    ///     The TrustKit Android Client Handler.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class TrustKitAndroidMessageHandler : AndroidMessageHandler
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="TrustKitAndroidMessageHandler"/>.
        /// </summary>
        public TrustKitAndroidMessageHandler() : base() { }

        /// <inheritdoc />
        protected override SSLSocketFactory ConfigureCustomSSLSocketFactory(HttpsURLConnection connection)
        {
            return Com.Datatheorem.Android.Trustkit.TrustKit.Instance.GetSSLSocketFactory(connection.URL.Host);
        }
    }
}