using System.Net.Http;
using Foundation;
using System.Collections.Generic;
using TrustKit.Xamarin.Core;
using System;

namespace TrustKit.Xamarin.iOS
{
    [Preserve(AllMembers = true)]
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory, IDisposable
    {
        public const string TSKConfigurationKey = "TSKConfiguration";
        private bool _isInitialized;
        private bool _disposedValue;

        public HttpMessageHandlerFactory() { }

        /// <summary>
        ///     Initialize the Shared Instance of TrustKit with the given configuration [NullAllowed].
        /// </summary>
        /// <param name="trustKitConfig">The TrustKit configuration. If null is passed, reads in keys from info.plist.</param>
        public void InitSharedInstanceWithConfiguration(NSDictionary<NSString, NSObject> trustKitConfig = default)
        {
            if (_isInitialized)
            {
                return;
            }

            if (trustKitConfig == null)
            {
                // read them in from info.plist
                NSDictionary trustKitDictionary = (NSDictionary)NSBundle.MainBundle.ObjectForInfoDictionary(TSKConfigurationKey);
                List<NSString> keys = new();
                foreach (NSObject key in trustKitDictionary.Keys)
                {
                    keys.Add((NSString)key);
                }
                NSDictionary<NSString, NSObject> value = new(keys.ToArray(), trustKitDictionary.Values);

                TrustKit.InitSharedInstanceWithConfiguration(value);
            }
            else
            {
                TrustKit.InitSharedInstanceWithConfiguration(trustKitConfig);
            }
            _isInitialized = true;
        }

        /// <inheritdoc />
        public virtual HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitiOSClientHandler();
        }

        /// <inheritdoc />
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    TrustKit.SharedInstance()?.Dispose();
                }
                _disposedValue = true;
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
