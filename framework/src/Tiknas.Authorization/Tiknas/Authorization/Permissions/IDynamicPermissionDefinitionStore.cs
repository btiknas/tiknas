using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tiknas.Authorization.Permissions;

public interface IDynamicPermissionDefinitionStore
{
    Task<PermissionDefinition?> GetOrNullAsync(string name);

    Task<IReadOnlyList<PermissionDefinition>> GetPermissionsAsync();
    
    Task<IReadOnlyList<PermissionGroupDefinition>> GetGroupsAsync();
}