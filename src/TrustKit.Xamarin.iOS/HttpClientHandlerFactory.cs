using System;
using System.Net.Http;
using Security;
using Foundation;
using System.Collections.Generic;
using TrustKit.Xamarin.Core;

namespace TrustKit.Xamarin.iOS
{
    [Preserve(AllMembers = true)]
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        private const string ConfigurationKey = "TSKConfiguration";

        public HttpMessageHandlerFactory() { }

        public static void Init()
        {
            NSDictionary trustKitConfig = (NSDictionary)NSBundle.MainBundle.ObjectForInfoDictionary(ConfigurationKey);

            List<NSString> keys = new List<NSString>();
            foreach (NSObject key in trustKitConfig.Keys)
            {
                keys.Add((NSString)key);
            }

            NSDictionary<NSString, NSObject> value = new NSDictionary<NSString, NSObject>(keys.ToArray(), trustKitConfig.Values);
            TrustKit.InitSharedInstanceWithConfiguration(value);
        }

        /// <inheritdoc />
        public HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitiOSClientHandler();
        }

        protected sealed class TrustKitiOSClientHandler : NSUrlSessionHandler
        {
            public TrustKitiOSClientHandler()
            {
                TrustOverrideForUrl += (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
            }

            private bool TrustKitOverrideHandler(string url, SecTrust trust)
            {
                Uri uri = new Uri(url);
                TSKTrustDecision decision = (TSKTrustDecision) TrustKit.SharedInstance().PinningValidator.EvaluateTrust(trust, uri.Host);

                switch (decision)
                {
                    case TSKTrustDecision.ShouldAllowConnection:
                        return true;
                    case TSKTrustDecision.ShouldBlockConnection:
                        return false;
                    case TSKTrustDecision.DomainNotPinned:
                        return true;
                    default:
                        return true;
                }
            }

            protected override void Dispose(bool disposing)
            {
                TrustOverrideForUrl-= (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
                base.Dispose(disposing);
            }
        }
    }
}
