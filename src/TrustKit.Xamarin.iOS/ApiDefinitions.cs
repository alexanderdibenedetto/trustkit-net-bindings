using System;
using Foundation;
using ObjCRuntime;
using Security;

namespace TrustKit.Xamarin.iOS
{

	// @interface TSKPinningValidatorResult
	interface TSKPinningValidatorResult
	{
		// @property (readonly, nonatomic) int * _Nonnull serverHostname;
		[Export("serverHostname")]
		unsafe int* ServerHostname { get; }

		// @property (readonly, nonatomic) int evaluationResult;
		[Export("evaluationResult")]
		int EvaluationResult { get; }

        // @property (readonly, nonatomic) TSKTrustDecision finalTrustDecision;
        [Export("finalTrustDecision")]
        long FinalTrustDecision { get; }

		// @property (readonly, nonatomic) int validationDuration;
		[Export("validationDuration")]
		int ValidationDuration { get; }

		// @property (readonly, nonatomic) int * _Nonnull certificateChain;
		[Export("certificateChain")]
		unsafe int* CertificateChain { get; }
	}

    // typedef void (^TSKPinningValidatorCallback)(TSKPinningValidatorResult * _Nonnull, int * _Nonnull, int * _Nonnull);
    unsafe delegate void TSKPinningValidatorCallback(TSKPinningValidatorResult arg0, Int32* arg1, Int32* arg2);

    // @interface TSKPinningValidator
    [BaseType(typeof(NSObject))]
    interface TSKPinningValidator
	{
        //// -(id)handleChallenge:(id)challenge completionHandler:(void (^ _Nonnull)(int, int * _Nullable))completionHandler;
        //[Export ("handleChallenge:completionHandler:")]
        //unsafe NSObject HandleChallenge (NSObject challenge, Action<int, int*> completionHandler);

        // -(TSKTrustDecision)evaluateTrust:(id)serverTrust forHostname:(id)serverHostname;
        [Export("evaluateTrust:forHostname:")]
        long EvaluateTrust(SecTrust serverTrust, string serverHostname);
    }

    // @interface TrustKit
    [BaseType(typeof(NSObject))]
    interface TrustKit
	{
		// +(void)initSharedInstanceWithConfiguration:(id)trustKitConfig;
		[Static]
		[Export("initSharedInstanceWithConfiguration:")]
		void InitSharedInstanceWithConfiguration(NSDictionary<NSString, NSObject> trustKitConfig);

		// +(void)initSharedInstanceWithConfiguration:(id)trustKitConfig sharedContainerIdentifier:(id)sharedContainerIdentifier;
		[Static]
		[Export("initSharedInstanceWithConfiguration:sharedContainerIdentifier:")]
		void InitSharedInstanceWithConfiguration(NSDictionary<NSString, NSObject> trustKitConfig, string sharedContainerIdentifier);

		// +(instancetype)sharedInstance;
		[Static]
		[Export("sharedInstance")]
		TrustKit SharedInstance();

		// @property (nonatomic) TSKPinningValidator * _Nonnull pinningValidator;
		[Export("pinningValidator", ArgumentSemantic.Assign)]
		TSKPinningValidator PinningValidator { get; set; }

		//// @property (nonatomic) TSKPinningValidatorCallback _Nullable pinningValidatorCallback;
		//[NullAllowed, Export("pinningValidatorCallback", ArgumentSemantic.Assign)]
		//TSKPinningValidatorCallback PinningValidatorCallback { get; set; }

		// @property (nonatomic, null_resettable) int pinningValidatorCallbackQueue;
		[NullAllowed, Export("pinningValidatorCallbackQueue")]
		int PinningValidatorCallbackQueue { get; set; }

		// -(instancetype)initWithConfiguration:(id)trustKitConfig;
		[Export("initWithConfiguration:")]
		IntPtr Constructor(NSObject trustKitConfig);

		// -(instancetype)initWithConfiguration:(id)trustKitConfig sharedContainerIdentifier:(id)sharedContainerIdentifier;
		[Export("initWithConfiguration:sharedContainerIdentifier:")]
		IntPtr Constructor(NSObject trustKitConfig, NSObject sharedContainerIdentifier);

		//// +(void)setLoggerBlock:(void (^)(int *))block;
		//[Static]
		//[Export ("setLoggerBlock:")]
		//unsafe void SetLoggerBlock (Action<int*> block);
	}
}