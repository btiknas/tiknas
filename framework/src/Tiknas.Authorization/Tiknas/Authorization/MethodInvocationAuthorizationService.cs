using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Tiknas.DependencyInjection;

namespace Tiknas.Authorization;

public class MethodInvocationAuthorizationService : IMethodInvocationAuthorizationService, ITransientDependency
{
    private readonly ITiknasAuthorizationPolicyProvider _tiknasAuthorizationPolicyProvider;
    private readonly ITiknasAuthorizationService _tiknasAuthorizationService;

    public MethodInvocationAuthorizationService(
        ITiknasAuthorizationPolicyProvider tiknasAuthorizationPolicyProvider,
        ITiknasAuthorizationService tiknasAuthorizationService)
    {
        _tiknasAuthorizationPolicyProvider = tiknasAuthorizationPolicyProvider;
        _tiknasAuthorizationService = tiknasAuthorizationService;
    }

    public async Task CheckAsync(MethodInvocationAuthorizationContext context)
    {
        if (AllowAnonymous(context))
        {
            return;
        }

        var authorizationPolicy = await AuthorizationPolicy.CombineAsync(
            _tiknasAuthorizationPolicyProvider,
            GetAuthorizationDataAttributes(context.Method)
        );

        if (authorizationPolicy == null)
        {
            return;
        }

        await _tiknasAuthorizationService.CheckAsync(authorizationPolicy);
    }

    protected virtual bool AllowAnonymous(MethodInvocationAuthorizationContext context)
    {
        return context.Method.GetCustomAttributes(true).OfType<IAllowAnonymous>().Any();
    }

    protected virtual IEnumerable<IAuthorizeData> GetAuthorizationDataAttributes(MethodInfo methodInfo)
    {
        var attributes = methodInfo
            .GetCustomAttributes(true)
            .OfType<IAuthorizeData>();

        if (methodInfo.IsPublic && methodInfo.DeclaringType != null)
        {
            attributes = attributes
                .Union(
                    methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<IAuthorizeData>()
                );
        }

        return attributes;
    }
}
