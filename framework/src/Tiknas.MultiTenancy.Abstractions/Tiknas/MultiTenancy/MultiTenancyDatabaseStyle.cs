using System;

namespace Tiknas.MultiTenancy;

[Flags]
public enum MultiTenancyDatabaseStyle
{
    Shared = 1,
    PerTenant = 2,
    Hybrid = Shared | PerTenant
}
