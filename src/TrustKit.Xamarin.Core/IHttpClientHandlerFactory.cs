using System.Net.Http;

namespace TrustKit.Xamarin.Core
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler BuildHttpMessageHandler();
    }
}
