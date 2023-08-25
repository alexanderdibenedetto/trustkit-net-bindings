using ObjCRuntime;

namespace TrustKit.Net.iOS
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