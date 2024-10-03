using System.Collections.Generic;
using System.Security.Claims;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Components.WebAssembly;

public class WebAssemblyRemoteCurrentPrincipalAccessor : CurrentPrincipalAccessorBase, ITransientDependency
{
    protected ApplicationConfigurationCache ApplicationConfigurationCache { get; }

    public WebAssemblyRemoteCurrentPrincipalAccessor(ApplicationConfigurationCache applicationConfigurationCache)
    {
        ApplicationConfigurationCache = applicationConfigurationCache;
    }

    protected override ClaimsPrincipal GetClaimsPrincipal()
    {
        var applicationConfiguration = ApplicationConfigurationCache.Get();
        if (applicationConfiguration == null || !applicationConfiguration.CurrentUser.IsAuthenticated)
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        var claims = new List<Claim>()
        {
            new Claim(TiknasClaimTypes.UserId, applicationConfiguration.CurrentUser.Id.ToString()!),
        };

        if (applicationConfiguration.CurrentUser.TenantId != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.TenantId, applicationConfiguration.CurrentUser.TenantId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorUserId != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.ImpersonatorUserId, applicationConfiguration.CurrentUser.ImpersonatorUserId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorTenantId != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.ImpersonatorTenantId, applicationConfiguration.CurrentUser.ImpersonatorTenantId.ToString()!));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorUserName != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.ImpersonatorUserName, applicationConfiguration.CurrentUser.ImpersonatorUserName));
        }
        if (applicationConfiguration.CurrentUser.ImpersonatorTenantName != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.ImpersonatorTenantName, applicationConfiguration.CurrentUser.ImpersonatorTenantName));
        }
        if (applicationConfiguration.CurrentUser.UserName != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.UserName, applicationConfiguration.CurrentUser.UserName));
        }
        if (applicationConfiguration.CurrentUser.Name != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.Name, applicationConfiguration.CurrentUser.Name));
        }
        if (applicationConfiguration.CurrentUser.SurName != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.SurName, applicationConfiguration.CurrentUser.SurName));
        }
        if (applicationConfiguration.CurrentUser.Email != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.Email, applicationConfiguration.CurrentUser.Email));
        }
        if (applicationConfiguration.CurrentUser.EmailVerified)
        {
            claims.Add(new Claim(TiknasClaimTypes.EmailVerified, applicationConfiguration.CurrentUser.EmailVerified.ToString()));
        }
        if (applicationConfiguration.CurrentUser.PhoneNumber != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.PhoneNumber, applicationConfiguration.CurrentUser.PhoneNumber));
        }
        if (applicationConfiguration.CurrentUser.PhoneNumberVerified)
        {
            claims.Add(new Claim(TiknasClaimTypes.PhoneNumberVerified, applicationConfiguration.CurrentUser.PhoneNumberVerified.ToString()));
        }
        if (applicationConfiguration.CurrentUser.SessionId != null)
        {
            claims.Add(new Claim(TiknasClaimTypes.SessionId, applicationConfiguration.CurrentUser.SessionId));
        }

        if (!applicationConfiguration.CurrentUser.Roles.IsNullOrEmpty())
        {
            foreach (var role in applicationConfiguration.CurrentUser.Roles)
            {
                claims.Add(new Claim(TiknasClaimTypes.Role, role));
            }
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, authenticationType: nameof(WebAssemblyRemoteCurrentPrincipalAccessor)));
    }
}
