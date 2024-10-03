using System;
using System.Collections.Generic;

namespace Tiknas.Http.Client.ClientProxying;

public class TiknasHttpClientProxyingOptions
{
    public Dictionary<Type, Type> QueryStringConverts { get; set; }

    public Dictionary<Type, Type> FormDataConverts { get; set; }

    public Dictionary<Type, Type> PathConverts { get; set; }

    public TiknasHttpClientProxyingOptions()
    {
        QueryStringConverts = new Dictionary<Type, Type>();
        FormDataConverts = new Dictionary<Type, Type>();
        PathConverts = new Dictionary<Type, Type>();
    }
}
