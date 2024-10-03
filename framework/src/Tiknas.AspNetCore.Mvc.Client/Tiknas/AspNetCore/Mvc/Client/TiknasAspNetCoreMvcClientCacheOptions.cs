using System;

namespace Tiknas.AspNetCore.Mvc.Client;

public class TiknasAspNetCoreMvcClientCacheOptions
{
    public TimeSpan ApplicationConfigurationDtoCacheAbsoluteExpiration { get; set; }

    public TiknasAspNetCoreMvcClientCacheOptions()
    {
        ApplicationConfigurationDtoCacheAbsoluteExpiration = TimeSpan.FromSeconds(300);
    }
}
