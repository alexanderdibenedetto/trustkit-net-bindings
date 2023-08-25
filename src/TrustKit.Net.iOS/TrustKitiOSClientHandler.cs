using Security;

namespace TrustKit.Net.iOS
{
    [Preserve(AllMembers = true)]
    public class TrustKitiOSClientHandler : NSUrlSessionHandler, IDisposable
    {
        private bool _disposedValue;

        public TrustKitiOSClientHandler()
        {
            TrustOverrideForUrl += (NSUrlSessionHandler sender, string url, SecTrust trust) => TrustKitOverrideHandler(url, trust);
        }

        private bool TrustKitOverrideHandler(string url, SecTrust trust)
        {
            Uri uri = new(url);
            TSKTrustDecision decision = (TSKTrustDecision)TrustKit.SharedInstance().PinningValidator.EvaluateTrust(trust, uri.Host);

            return decision switch
            {
                TSKTrustDecision.ShouldAllowConnection => true,
                TSKTrustDecision.ShouldBlockConnection => false,
                TSKTrustDecision.DomainNotPinned => true,
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