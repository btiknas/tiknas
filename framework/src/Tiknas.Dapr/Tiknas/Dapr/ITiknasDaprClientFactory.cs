using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dapr.Client;

namespace Tiknas.Dapr;

public interface ITiknasDaprClientFactory
{
    Task<DaprClient> CreateAsync(Action<DaprClientBuilder>? builderAction = null);

    Task<HttpClient> CreateHttpClientAsync(
        string? appId = null,
        string? daprEndpoint = null,
        string? daprApiToken = null
    );
}
