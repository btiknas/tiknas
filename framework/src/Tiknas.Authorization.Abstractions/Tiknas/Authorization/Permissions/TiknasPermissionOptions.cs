using System.Collections.Generic;
using Tiknas.Collections;

namespace Tiknas.Authorization.Permissions;

public class TiknasPermissionOptions
{
    public ITypeList<IPermissionDefinitionProvider> DefinitionProviders { get; }

    public ITypeList<IPermissionValueProvider> ValueProviders { get; }

    public HashSet<string> DeletedPermissions { get; }

    public HashSet<string> DeletedPermissionGroups { get; }

    public TiknasPermissionOptions()
    {
        DefinitionProviders = new TypeList<IPermissionDefinitionProvider>();
        ValueProviders = new TypeList<IPermissionValueProvider>();

        DeletedPermissions = new HashSet<string>();
        DeletedPermissionGroups = new HashSet<string>();
    }
}
