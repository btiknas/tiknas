using System;
using System.Collections.Generic;
using Tiknas.Http.Client.DynamicProxying;
using Tiknas.Http.Client.Proxying;

namespace Tiknas.Http.Client;

public class TiknasHttpClientOptions
{
    public Dictionary<Type, HttpClientProxyConfig> HttpClientProxies { get; set; }

    public TiknasHttpClientOptions()
    {
        HttpClientProxies = new Dictionary<Type, HttpClientProxyConfig>();
    }
}
