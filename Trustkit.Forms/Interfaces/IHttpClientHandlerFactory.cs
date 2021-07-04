using System;
using System.Net.Http;

namespace Trustkit.Forms.Interfaces
{
    public interface IHttpMessageHandlerFactory
    {
        HttpMessageHandler BuildHttpMessageHandler();
    }
}
