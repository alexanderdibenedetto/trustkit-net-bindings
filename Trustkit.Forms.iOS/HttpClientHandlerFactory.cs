using System;
using System.Net;
using System.Net.Http;
using Trustkit.Forms.Interfaces;
using Xamarin.Forms;
using TrustKit.Xamarin.iOS;
using Security;

[assembly: Dependency(typeof(Trustkit.Forms.iOS.HttpMessageHandlerFactory))]
namespace Trustkit.Forms.iOS
{
    public class HttpMessageHandlerFactory : IHttpMessageHandlerFactory
    {
        public HttpMessageHandler BuildHttpMessageHandler()
        {
            return new TrustKitiOSClientHandler();
        }

        protected class TrustKitiOSClientHandler : NSUrlSessionHandler
        {
            public TrustKitiOSClientHandler()
            {
                TrustOverrideForUrl += (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
            }

            private bool TrustKitOverrideHandler(string url, SecTrust trust)
            {
                TSKTrustDecision decision = TrustKit.Xamarin.iOS.TrustKit.SharedInstance().PinningValidator.EvaluateTrust(trust, url);

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
        }
    }
}
