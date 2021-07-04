# Xamarin Bindings for Certificate Pinning in iOS, Android
using DataTheorem's Trustkit and Trustkit-android repositories

The original platform libraries can be found below

[iOS]
https://github.com/datatheorem/TrustKit

[Android]
https://github.com/datatheorem/TrustKit-Android

# Usage
The basic interface for this library is to override the HttpMessageHandler on a System.Net.Http.HttpClient constructor with a TrustKit enhanced version. Use any dependency service you want, the following example uses the built in Xamarin.Forms dependency service. This builds the platform specific http message handler for any http clients needed in the shared code library.

```
IHttpMessageHandlerFactory httpMessageHandlerFactory = DependencyService.Resolve<IHttpMessageHandlerFactory>();
_httpClient = new HttpClient(httpMessageHandlerFactory.BuildHttpMessageHandler());
```

## iOS
For iOS, configure by placing the TrustKit configuration in the info.plist as described by the original TrustKit library. It will not use swizzling in this implementation, even though it is in the info.plist. Only the HttpClients with the TrustKit HttpMessageHandler parameter will use the certificate pinning implementation.

In the AppDelegate's FinishedLaunching method, be sure to put:
```
HttpMessageHandlerFactory.Init();
```
This initializes the TrustKit iOS code.

## Android
For Android, configure by placing the TrustKit configure in the normal Android security configuration file (resources/xml/network_security_config.xml) and be sure to follow the remaining documentation as described by the original TrustKit library.

In the MainActivity's OnCreate function, be sure to put:
```
HttpMessageHandlerFactory.Init(this);
```
This initializes the TrustKit Android code.

# About
Certificate pinning is an important mobile security technique (https://owasp.org/www-community/controls/Certificate_and_Public_Key_Pinning). It allows application developers to ship a known certificate value, (or certificate public key value) with their application. Then, during TLS handshaking to establish a secure HTTPS connection with a remote server, this shipped known value is compared to the value presented by the server (which will be used for decrypting packets). If the value matches, the mobile application developer allows the connection to continue. If it doesn't, the developer can stop the connection and handle the failure however they deem appropriate (such as notifying the user).

# Legacy
Certificate Pinning in Xamarin is relatively straightforward using the (now) legacy System.Net.WebClient class (https://docs.microsoft.com/en-us/dotnet/api/system.net.webclient) and the ServicePointManager (https://docs.microsoft.com/en-us/dotnet/api/system.net.servicepointmanager). Pass a delegate function to the RemoteCertificateValidationCallback, and all of the hard work of certifcate pinning is done. Similarly for the System.Net.HttpWebRequest class (https://docs.microsoft.com/en-us/dotnet/api/system.net.httpwebrequest) and its RemoteCertificateValidationCallback.

# Motivation
This legacy technique is fine, but it doesn't allow use of the newer System.Net.HttpClient (https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient) class and its impressive list of platform-dependent backend handlers. These handlers can be extended in a number of ways to allow for compression and other optimizations such as the ones Jonathan lists here (http://jonathanpeppers.com/Blog/improving-http-performance-in-xamarin-applications). One important way to extend these handlers - and the motivation of this repository - is certificate pinning via platform dependent library implementations by SecureTheorem.

# Thanks
Thanks to all of you for your assistance and interest in maintaining and fixing issues with this repository!
