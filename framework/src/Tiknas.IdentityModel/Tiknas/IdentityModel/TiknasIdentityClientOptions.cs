using System;
using System.Collections.Generic;
using System.Linq;
using Tiknas.MultiTenancy;

namespace Tiknas.IdentityModel;

public class TiknasIdentityClientOptions
{
    public IdentityClientConfigurationDictionary IdentityClients { get; set; }

    public TiknasIdentityClientOptions()
    {
        IdentityClients = new IdentityClientConfigurationDictionary();
    }

    public IdentityClientConfiguration? GetClientConfiguration(ICurrentTenant currentTenant, string? identityClientName = null)
    {
        if (identityClientName.IsNullOrWhiteSpace())
        {
            identityClientName = IdentityClientConfigurationDictionary.DefaultName;
        }

        if (currentTenant.Id.HasValue)
        {
            var tenantConfiguration = IdentityClients.FirstOrDefault(x => x.Key == $"{identityClientName}.{currentTenant.Id}");
            if (tenantConfiguration.Key == null && !currentTenant.Name.IsNullOrWhiteSpace())
            {
                tenantConfiguration = IdentityClients.FirstOrDefault(x => x.Key == $"{identityClientName}.{currentTenant.Name}");
            }

            if (tenantConfiguration.Key != null)
            {
                return tenantConfiguration.Value;
            }
        }

        return IdentityClients.GetOrDefault(identityClientName!) ??
               IdentityClients.Default;
    }
}
