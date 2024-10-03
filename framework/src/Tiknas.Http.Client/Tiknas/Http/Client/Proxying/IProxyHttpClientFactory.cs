using System.Net.Http;

namespace Tiknas.Http.Client.Proxying;

public interface IProxyHttpClientFactory
{
    HttpClient Create();

    HttpClient Create(string name);
}
