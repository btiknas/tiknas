using System;
using System.Collections.Generic;

namespace Tiknas.Cli.ServiceProxying;

public class TiknasCliServiceProxyOptions
{
    public IDictionary<string, Type> Generators { get; }

    public TiknasCliServiceProxyOptions()
    {
        Generators = new Dictionary<string, Type>();
    }
}
