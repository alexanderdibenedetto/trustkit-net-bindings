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
            try
            {
                // return true for loading if url is null or empty.
                if (string.IsNullOrEmpty(url))
                {
                    return true;
                }

                // return true for loading if trustkit instance is null or empty.
                if(iOS.Bindings.TrustKit.SharedInstance() == null)
                {
                    return true;
                }

                Uri uri = new(url);
                iOS.Bindings.TSKTrustDecision decision = iOS.Bindings.TrustKit.SharedInstance().PinningValidator?.EvaluateTrust(trust, uri.Host)
                    ?? iOS.Bindings.TSKTrustDecision.ShouldBlockConnection;

                return decision switch
                {
                    iOS.Bindings.TSKTrustDecision.ShouldAllowConnection => true,
                    iOS.Bindings.TSKTrustDecision.ShouldBlockConnection => false,
                    iOS.Bindings.TSKTrustDecision.DomainNotPinned => true,
                    _ => true,
                };
            }
            catch(Exception)
            {
                // For security reasons return false for any exception caught.
                return false;
            }
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