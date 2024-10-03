using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.AspNetCore.RequestLocalization;

public class TiknasRequestLocalizationOptions
{
    public List<Func<IServiceProvider, RequestLocalizationOptions, Task>> RequestLocalizationOptionConfigurators { get; }

    public TiknasRequestLocalizationOptions()
    {
        RequestLocalizationOptionConfigurators = new List<Func<IServiceProvider, RequestLocalizationOptions, Task>>();
    }
}
