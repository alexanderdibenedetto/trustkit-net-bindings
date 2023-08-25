namespace TrustKit.Net.Core
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler BuildHttpMessageHandler();
    }
}
