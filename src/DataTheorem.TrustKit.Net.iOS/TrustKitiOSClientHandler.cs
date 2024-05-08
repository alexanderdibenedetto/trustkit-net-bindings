using Security;

namespace DataTheorem.TrustKit.Net.iOS
{
    [Preserve(AllMembers = true)]
    public class TrustKitiOSClientHandler : NSUrlSessionHandler, IDisposable
    {
        private bool _disposedValue;

        public TrustKitiOSClientHandler() : base()
        {
            TrustOverrideForUrl += (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
        }

        public TrustKitiOSClientHandler(NSUrlSessionConfiguration configuration) : base(configuration)
        {
            TrustOverrideForUrl += (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
        }

        private bool TrustKitOverrideHandler(string url, SecTrust trust)
        {
            Uri uri = new(url);
            iOS.Bindings.TSKTrustDecision decision = iOS.Bindings.TrustKit.SharedInstance().PinningValidator.EvaluateTrust(trust, uri.Host);

            return decision switch
            {
                iOS.Bindings.TSKTrustDecision.ShouldAllowConnection => true,
                iOS.Bindings.TSKTrustDecision.ShouldBlockConnection => false,
                iOS.Bindings.TSKTrustDecision.DomainNotPinned => true,
                _ => true,
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    TrustOverrideForUrl -= (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
                }
                _disposedValue = true;
            }
            base.Dispose(disposing);
        }
    }
}