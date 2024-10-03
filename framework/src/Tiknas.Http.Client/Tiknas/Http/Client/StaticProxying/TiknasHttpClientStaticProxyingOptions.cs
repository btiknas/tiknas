using System;
using System.Collections.Generic;

namespace Tiknas.Http.Client.StaticProxying;

public class TiknasHttpClientStaticProxyingOptions
{
    public List<Type> BindingFromQueryTypes { get; }

    public TiknasHttpClientStaticProxyingOptions()
    {
        BindingFromQueryTypes = new List<Type>();
    }
}
