using System;
using System.Collections.Generic;

namespace Tiknas.Http.ProxyScripting.Configuration;

public class TiknasApiProxyScriptingOptions
{
    public IDictionary<string, Type> Generators { get; }

    public TiknasApiProxyScriptingOptions()
    {
        Generators = new Dictionary<string, Type>();
    }
}
