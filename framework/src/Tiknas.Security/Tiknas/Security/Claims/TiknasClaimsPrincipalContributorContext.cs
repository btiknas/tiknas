using System;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Tiknas.Security.Claims;

public class TiknasClaimsPrincipalContributorContext
{
    [NotNull]
    public ClaimsPrincipal ClaimsPrincipal { get; set; }

    [NotNull]
    public IServiceProvider ServiceProvider { get; }

    public TiknasClaimsPrincipalContributorContext(
        [NotNull] ClaimsPrincipal claimsIdentity,
        [NotNull] IServiceProvider serviceProvider)
    {
        ClaimsPrincipal = claimsIdentity;
        ServiceProvider = serviceProvider;
    }

    public virtual T GetRequiredService<T>()
        where T : notnull
    {
        Check.NotNull(ServiceProvider, nameof(ServiceProvider));
        return ServiceProvider.GetRequiredService<T>();
    }
}
