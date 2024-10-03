using System.Collections.Generic;
using System.Threading.Tasks;
using Tiknas;
using Tiknas.Authorization;

namespace Microsoft.AspNetCore.Authorization;

public static class TiknasAuthorizationServiceExtensions
{
    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, string policyName)
    {
        return await AuthorizeAsync(
            authorizationService,
            null,
            policyName
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsTiknasAuthorizationService().CurrentPrincipal,
            resource,
            requirement
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object? resource, AuthorizationPolicy policy)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsTiknasAuthorizationService().CurrentPrincipal,
            resource,
            policy
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        return await AuthorizeAsync(
            authorizationService,
            null,
            policy
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsTiknasAuthorizationService().CurrentPrincipal,
            resource,
            requirements
        );
    }

    public static async Task<AuthorizationResult> AuthorizeAsync(this IAuthorizationService authorizationService, object? resource, string policyName)
    {
        return await authorizationService.AuthorizeAsync(
            authorizationService.AsTiknasAuthorizationService().CurrentPrincipal,
            resource,
            policyName
        );
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, string policyName)
    {
        return (await authorizationService.AuthorizeAsync(policyName)).Succeeded;
    }

    public static async Task<bool> IsGrantedAnyAsync(
        this IAuthorizationService authorizationService,
        params string[] policyNames)
    {
        Check.NotNullOrEmpty(policyNames, nameof(policyNames));

        foreach (var policyName in policyNames)
        {
            if ((await authorizationService.AuthorizeAsync(policyName)).Succeeded)
            {
                return true;
            }
        }

        return false;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        return (await authorizationService.AuthorizeAsync(resource, requirement)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy)
    {
        return (await authorizationService.AuthorizeAsync(resource, policy)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        return (await authorizationService.AuthorizeAsync(policy)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        return (await authorizationService.AuthorizeAsync(resource, requirements)).Succeeded;
    }

    public static async Task<bool> IsGrantedAsync(this IAuthorizationService authorizationService, object resource, string policyName)
    {
        return (await authorizationService.AuthorizeAsync(resource, policyName)).Succeeded;
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, string policyName)
    {
        if (!await authorizationService.IsGrantedAsync(policyName))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGrantedWithPolicyName)
                .WithData("PolicyName", policyName);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, IAuthorizationRequirement requirement)
    {
        if (!await authorizationService.IsGrantedAsync(resource, requirement))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenRequirementHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, AuthorizationPolicy policy)
    {
        if (!await authorizationService.IsGrantedAsync(resource, policy))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, AuthorizationPolicy policy)
    {
        if (!await authorizationService.IsGrantedAsync(policy))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGranted);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, IEnumerable<IAuthorizationRequirement> requirements)
    {
        if (!await authorizationService.IsGrantedAsync(resource, requirements))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenRequirementsHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    public static async Task CheckAsync(this IAuthorizationService authorizationService, object resource, string policyName)
    {
        if (!await authorizationService.IsGrantedAsync(resource, policyName))
        {
            throw new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", resource);
        }
    }

    private static ITiknasAuthorizationService AsTiknasAuthorizationService(this IAuthorizationService authorizationService)
    {
        if (!(authorizationService is ITiknasAuthorizationService tiknasAuthorizationService))
        {
            throw new TiknasException($"{nameof(authorizationService)} should implement {typeof(ITiknasAuthorizationService).FullName}");
        }

        return tiknasAuthorizationService;
    }
}
