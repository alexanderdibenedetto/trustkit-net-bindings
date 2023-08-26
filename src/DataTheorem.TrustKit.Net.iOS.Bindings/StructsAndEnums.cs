using ObjCRuntime;

namespace DataTheorem.TrustKit.Net.iOS.Bindings
{
    [Native]
    public enum TSKTrustEvaluationResult : long
    {
        Success,
        FailedNoMatchingPin,
        FailedInvalidCertificateChain,
        ErrorInvalidParameters,
        FailedUserDefinedTrustAnchor,
        ErrorCouldNotGenerateSpkiHash
    }

    [Native]
    public enum TSKTrustDecision : long
    {
        ShouldAllowConnection,
        ShouldBlockConnection,
        DomainNotPinned
    }
}