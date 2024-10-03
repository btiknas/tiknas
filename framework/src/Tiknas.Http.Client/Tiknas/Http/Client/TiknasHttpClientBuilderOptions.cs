using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Tiknas.Http.Client;

public class TiknasHttpClientBuilderOptions
{
    public List<Action<string, IHttpClientBuilder>> ProxyClientBuildActions { get; }

    internal HashSet<string> ConfiguredProxyClients { get; }

    public List<Action<string, IServiceProvider, HttpClient>> ProxyClientActions { get; }
    
    public List<Action<string, IServiceProvider, HttpClientHandler>> ProxyClientHandlerActions { get; }

    public TiknasHttpClientBuilderOptions()
    {
        ProxyClientBuildActions = new List<Action<string, IHttpClientBuilder>>();
        ConfiguredProxyClients = new HashSet<string>();
        ProxyClientActions = new List<Action<string, IServiceProvider, HttpClient>>();
        ProxyClientHandlerActions = new List<Action<string, IServiceProvider, HttpClientHandler>>();
    }
}
