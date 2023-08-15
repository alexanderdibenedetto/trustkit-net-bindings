using System.Net.Http;
using Android.Content;
using TrustKit.Xamarin.Core;
using Android.Runtime;
using System;

namespace TrustKit.Xamarin.Android
{
    /// <summary>
    ///     The Http Message Handler Factory for Android.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory, IDisposable
    {
        private bool _initialized = false;
        private bool _disposedValue = false;

        /// <summary>
        ///     Initializes a new instance of the HttpMessageHandlerFactory.
        /// </summary>
        public HttpMessageHandlerFactory() { }

        /// <summary>
        ///     Initializes TrustKit on the Xamarin.Android platform.
        /// </summary>
        /// <param name="context"></param>
        public void InitializeWithNetworkSecurityConfiguration(Context context)
        {
            if (_initialized)
            {
                return;
            }

            Com.Datatheorem.Android.Trustkit.TrustKit.InitializeWithNetworkSecurityConfiguration(context);
            _initialized = true;
        }

        /// <inheritdoc />
        public virtual HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitAndroidClientHandler();
        }

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Com.Datatheorem.Android.Trustkit.TrustKit.Instance?.Dispose();
                }
                _disposedValue = true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
