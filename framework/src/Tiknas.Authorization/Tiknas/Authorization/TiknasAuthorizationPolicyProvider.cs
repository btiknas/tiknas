using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Tiknas.Authorization.Permissions;
using Tiknas.DependencyInjection;

namespace Tiknas.Authorization;

public class TiknasAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, ITiknasAuthorizationPolicyProvider, ITransientDependency
{
    private readonly AuthorizationOptions _options;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;

    public TiknasAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options,
        IPermissionDefinitionManager permissionDefinitionManager)
        : base(options)
    {
        _permissionDefinitionManager = permissionDefinitionManager;
        _options = options.Value;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy != null)
        {
            return policy;
        }

        var permission = await _permissionDefinitionManager.GetOrNullAsync(policyName);
        if (permission != null)
        {
            //TODO: Optimize & Cache!
            var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
            policyBuilder.Requirements.Add(new PermissionRequirement(policyName));
            return policyBuilder.Build();
        }

        return null;
    }

    public async Task<List<string>> GetPoliciesNamesAsync()
    {
        return _options.GetPoliciesNames()
            .Union(
                (await _permissionDefinitionManager
                    .GetPermissionsAsync())
                .Select(p => p.Name)
            )
            .ToList();
    }
}
