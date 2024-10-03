using System.Collections.Generic;

namespace Tiknas.Authorization.Permissions;

public interface IPermissionValueProviderManager
{
    IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
}
