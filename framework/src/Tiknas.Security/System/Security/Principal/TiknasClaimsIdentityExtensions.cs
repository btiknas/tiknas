﻿using System.Linq;
using System.Security.Claims;
using JetBrains.Annotations;
using Tiknas;
using Tiknas.Security.Claims;

namespace System.Security.Principal;

public static class TiknasClaimsIdentityExtensions
{
    public static Guid? FindUserId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var userIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.UserId);
        if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(userIdOrNull.Value, out Guid guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindUserId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var userIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.UserId);
        if (userIdOrNull == null || userIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(userIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindTenantId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var tenantIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.TenantId);
        if (tenantIdOrNull == null || tenantIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(tenantIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindTenantId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var tenantIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.TenantId);
        if (tenantIdOrNull == null || tenantIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(tenantIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static string? FindClientId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var clientIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ClientId);
        if (clientIdOrNull == null || clientIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        return clientIdOrNull.Value;
    }

    public static string? FindClientId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var clientIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ClientId);
        if (clientIdOrNull == null || clientIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        return clientIdOrNull.Value;
    }

    public static Guid? FindEditionId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var editionIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.EditionId);
        if (editionIdOrNull == null || editionIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(editionIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindEditionId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var editionIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.EditionId);
        if (editionIdOrNull == null || editionIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(editionIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindImpersonatorTenantId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var impersonatorTenantIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ImpersonatorTenantId);
        if (impersonatorTenantIdOrNull == null || impersonatorTenantIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(impersonatorTenantIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindImpersonatorTenantId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var impersonatorTenantIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ImpersonatorTenantId);
        if (impersonatorTenantIdOrNull == null || impersonatorTenantIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(impersonatorTenantIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindImpersonatorUserId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var impersonatorUserIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ImpersonatorUserId);
        if (impersonatorUserIdOrNull == null || impersonatorUserIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(impersonatorUserIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static Guid? FindImpersonatorUserId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var impersonatorUserIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.ImpersonatorUserId);
        if (impersonatorUserIdOrNull == null || impersonatorUserIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        if (Guid.TryParse(impersonatorUserIdOrNull.Value, out var guid))
        {
            return guid;
        }

        return null;
    }

    public static ClaimsIdentity AddIfNotContains(this ClaimsIdentity claimsIdentity, Claim claim)
    {
        Check.NotNull(claimsIdentity, nameof(claimsIdentity));

        if (!claimsIdentity.Claims.Any(x => string.Equals(x.Type, claim.Type, StringComparison.OrdinalIgnoreCase)))
        {
            claimsIdentity.AddClaim(claim);
        }

        return claimsIdentity;
    }

    public static ClaimsIdentity RemoveAll(this ClaimsIdentity claimsIdentity, string claimType)
    {
        Check.NotNull(claimsIdentity, nameof(claimsIdentity));

        foreach (var x in claimsIdentity.FindAll(claimType).ToList())
        {
            claimsIdentity.RemoveClaim(x);
        }

        return claimsIdentity;
    }


    public static ClaimsIdentity AddOrReplace(this ClaimsIdentity claimsIdentity, Claim claim)
    {
        Check.NotNull(claimsIdentity, nameof(claimsIdentity));

        foreach (var x in claimsIdentity.FindAll(claim.Type).ToList())
        {
            claimsIdentity.RemoveClaim(x);
        }

        claimsIdentity.AddClaim(claim);

        return claimsIdentity;
    }

    public static ClaimsPrincipal AddIdentityIfNotContains([NotNull] this ClaimsPrincipal principal, ClaimsIdentity identity)
    {
        Check.NotNull(principal, nameof(principal));

        if (!principal.Identities.Any(x => string.Equals(x.AuthenticationType, identity.AuthenticationType, StringComparison.OrdinalIgnoreCase)))
        {
            principal.AddIdentity(identity);
        }

        return principal;
    }

    public static string? FindSessionId([NotNull] this IIdentity identity)
    {
        Check.NotNull(identity, nameof(identity));

        var claimsIdentity = identity as ClaimsIdentity;

        var sessionIdOrNull = claimsIdentity?.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.SessionId);
        if (sessionIdOrNull == null || sessionIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        return sessionIdOrNull.Value;
    }

    public static string? FindSessionId([NotNull] this ClaimsPrincipal principal)
    {
        Check.NotNull(principal, nameof(principal));

        var sessionIdOrNull = principal.Claims?.FirstOrDefault(c => c.Type == TiknasClaimTypes.SessionId);
        if (sessionIdOrNull == null || sessionIdOrNull.Value.IsNullOrWhiteSpace())
        {
            return null;
        }

        return sessionIdOrNull.Value;
    }
}
