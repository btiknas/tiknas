using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tiknas.Authorization;
using Tiknas.Authorization.Permissions;

namespace Microsoft.Extensions.DependencyInjection;

public static class TiknasAuthorizationServiceCollectionExtensions
{
    public static IServiceCollection AddAlwaysAllowAuthorization(this IServiceCollection services)
    {
        services.Replace(ServiceDescriptor.Singleton<IAuthorizationService, AlwaysAllowAuthorizationService>());
        services.Replace(ServiceDescriptor.Singleton<ITiknasAuthorizationService, AlwaysAllowAuthorizationService>());
        services.Replace(ServiceDescriptor.Singleton<IMethodInvocationAuthorizationService, AlwaysAllowMethodInvocationAuthorizationService>());
        return services.Replace(ServiceDescriptor.Singleton<IPermissionChecker, AlwaysAllowPermissionChecker>());
    }
}
