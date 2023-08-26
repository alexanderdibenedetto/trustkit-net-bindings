using System;
using CoreFoundation;
using Foundation;
using ObjCRuntime;
using Security;

namespace DataTheorem.TrustKit.Net.iOS.Bindings
{
    // typedef void (^TSKPinningValidatorCallback)(TSKPinningValidatorResult * _Nonnull, NSString * _Nonnull, TKSDomainPinningPolicy * _Nonnull);
    delegate void TSKPinningValidatorCallback(TSKPinningValidatorResult arg0, string arg1, NSDictionary<NSString, NSObject> arg2);

    // @interface TSKPinningValidatorResult : NSObject
    [BaseType(typeof(NSObject))]
    interface TSKPinningValidatorResult
    {
        // @property (readonly, nonatomic) NSString * _Nonnull serverHostname;
        [Export("serverHostname")]
        string ServerHostname { get; }

        // @property (readonly, nonatomic) TSKTrustEvaluationResult evaluationResult;
        [Export("evaluationResult")]
        TSKTrustEvaluationResult EvaluationResult { get; }

        // @property (readonly, nonatomic) TSKTrustDecision finalTrustDecision;
        [Export("finalTrustDecision")]
        TSKTrustDecision FinalTrustDecision { get; }

        // @property (readonly, nonatomic) NSTimeInterval validationDuration;
        [Export("validationDuration")]
        double ValidationDuration { get; }

        // @property (readonly, nonatomic) NSArray * _Nonnull certificateChain;
        [Export("certificateChain")]
        NSObject[] CertificateChain { get; }
    }

    // @interface TSKPinningValidator : NSObject
    [BaseType(typeof(NSObject))]
    interface TSKPinningValidator
    {
        // -(BOOL)handleChallenge:(NSURLAuthenticationChallenge * _Nonnull)challenge completionHandler:(void (^ _Nonnull)(NSURLSessionAuthChallengeDisposition, NSURLCredential * _Nullable))completionHandler;
        [Export("handleChallenge:completionHandler:")]
        bool HandleChallenge(NSUrlAuthenticationChallenge challenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler);

        // -(TSKTrustDecision)evaluateTrust:(SecTrustRef _Nonnull)serverTrust forHostname:(NSString * _Nonnull)serverHostname;
        [Export("evaluateTrust:forHostname:")]
        TSKTrustDecision EvaluateTrust(SecTrust serverTrust, string serverHostname);
    }

    // @interface TrustKit : NSObject
    [BaseType(typeof(NSObject))]
    interface TrustKit
    {
        // +(void)initSharedInstanceWithConfiguration:(NSDictionary<TSKGlobalConfigurationKey,id> * _Nonnull)trustKitConfig;
        [Static]
        [Export("initSharedInstanceWithConfiguration:")]
        void InitSharedInstanceWithConfiguration(NSDictionary<NSString, NSObject> trustKitConfig);

        // +(void)initSharedInstanceWithConfiguration:(NSDictionary<TSKGlobalConfigurationKey,id> * _Nonnull)trustKitConfig sharedContainerIdentifier:(NSString * _Nullable)sharedContainerIdentifier;
        [Static]
        [Export("initSharedInstanceWithConfiguration:sharedContainerIdentifier:")]
        void InitSharedInstanceWithConfiguration(NSDictionary<NSString, NSObject> trustKitConfig, [NullAllowed] string sharedContainerIdentifier);

        // +(instancetype _Nonnull)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        TrustKit SharedInstance();

        // @property (nonatomic) TSKPinningValidator * _Nonnull pinningValidator;
        [Export("pinningValidator", ArgumentSemantic.Assign)]
        TSKPinningValidator PinningValidator { get; set; }

        // @property (nonatomic) TSKPinningValidatorCallback _Nullable pinningValidatorCallback;
        [NullAllowed, Export("pinningValidatorCallback", ArgumentSemantic.Assign)]
        TSKPinningValidatorCallback PinningValidatorCallback { get; set; }

        // @property (nonatomic, null_resettable) dispatch_queue_t _Null_unspecified pinningValidatorCallbackQueue;
        [NullAllowed, Export("pinningValidatorCallbackQueue", ArgumentSemantic.Assign)]
        DispatchQueue PinningValidatorCallbackQueue { get; set; }

        // -(instancetype _Nonnull)initWithConfiguration:(NSDictionary<TSKGlobalConfigurationKey,id> * _Nonnull)trustKitConfig;
        [Export("initWithConfiguration:")]
        IntPtr Constructor(NSDictionary<NSString, NSObject> trustKitConfig);

        // -(instancetype _Nonnull)initWithConfiguration:(NSDictionary<TSKGlobalConfigurationKey,id> * _Nonnull)trustKitConfig sharedContainerIdentifier:(NSString * _Nullable)sharedContainerIdentifier;
        [Export("initWithConfiguration:sharedContainerIdentifier:")]
        IntPtr Constructor(NSDictionary<NSString, NSObject> trustKitConfig, [NullAllowed] string sharedContainerIdentifier);

        // +(void) setLoggerBlock:(void (^ _Nonnull)(NSString* _Nonnull))block;
        [Static]
        [Export("setLoggerBlock:")]
        void SetLoggerBlock(Action<NSString> block);
    }
}