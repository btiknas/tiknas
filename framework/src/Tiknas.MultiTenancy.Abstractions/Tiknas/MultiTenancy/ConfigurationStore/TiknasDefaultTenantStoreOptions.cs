using System;

namespace Tiknas.MultiTenancy.ConfigurationStore;

public class TiknasDefaultTenantStoreOptions
{
    public TenantConfiguration[] Tenants { get; set; }

    public TiknasDefaultTenantStoreOptions()
    {
        Tenants = Array.Empty<TenantConfiguration>();
    }
}
