using System.Net.Http;

namespace TrustKit.Xamarin
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler BuildHttpMessageHandler();
    }
}
