using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Tiknas.DependencyInjection;
using Tiknas.Security.Claims;

namespace Tiknas.Users;

public class CurrentUser : ICurrentUser, ITransientDependency
{
    private static readonly Claim[] EmptyClaimsArray = new Claim[0];

    public virtual bool IsAuthenticated => Id.HasValue;

    public virtual Guid? Id => _principalAccessor.Principal?.FindUserId();

    public virtual string? UserName => this.FindClaimValue(TiknasClaimTypes.UserName);

    public virtual string? Name => this.FindClaimValue(TiknasClaimTypes.Name);

    public virtual string? SurName => this.FindClaimValue(TiknasClaimTypes.SurName);

    public virtual string? PhoneNumber => this.FindClaimValue(TiknasClaimTypes.PhoneNumber);

    public virtual bool PhoneNumberVerified => string.Equals(this.FindClaimValue(TiknasClaimTypes.PhoneNumberVerified), "true", StringComparison.InvariantCultureIgnoreCase);

    public virtual string? Email => this.FindClaimValue(TiknasClaimTypes.Email);

    public virtual bool EmailVerified => string.Equals(this.FindClaimValue(TiknasClaimTypes.EmailVerified), "true", StringComparison.InvariantCultureIgnoreCase);

    public virtual Guid? TenantId => _principalAccessor.Principal?.FindTenantId();

    public virtual string[] Roles => FindClaims(TiknasClaimTypes.Role).Select(c => c.Value).Distinct().ToArray();

    private readonly ICurrentPrincipalAccessor _principalAccessor;

    public CurrentUser(ICurrentPrincipalAccessor principalAccessor)
    {
        _principalAccessor = principalAccessor;
    }

    public virtual Claim? FindClaim(string claimType)
    {
        return _principalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public virtual Claim[] FindClaims(string claimType)
    {
        return _principalAccessor.Principal?.Claims.Where(c => c.Type == claimType).ToArray() ?? EmptyClaimsArray;
    }

    public virtual Claim[] GetAllClaims()
    {
        return _principalAccessor.Principal?.Claims.ToArray() ?? EmptyClaimsArray;
    }

    public virtual bool IsInRole(string roleName)
    {
        return FindClaims(TiknasClaimTypes.Role).Any(c => c.Value == roleName);
    }
}
