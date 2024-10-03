using System.Collections.Generic;
using System.Security.Claims;
using Tiknas.Collections;

namespace Tiknas.Security.Claims;

public class TiknasClaimsPrincipalFactoryOptions
{
    public ITypeList<ITiknasClaimsPrincipalContributor> Contributors { get; }

    public ITypeList<ITiknasDynamicClaimsPrincipalContributor> DynamicContributors { get; }

    public List<string> DynamicClaims { get; }

    public bool IsRemoteRefreshEnabled { get; set; }

    public string RemoteRefreshUrl { get; set; }

    public Dictionary<string, List<string>> ClaimsMap { get; set; }

    public bool IsDynamicClaimsEnabled { get; set; }

    public TiknasClaimsPrincipalFactoryOptions()
    {
        Contributors = new TypeList<ITiknasClaimsPrincipalContributor>();
        DynamicContributors = new TypeList<ITiknasDynamicClaimsPrincipalContributor>();

        DynamicClaims = new List<string>
        {
            TiknasClaimTypes.UserName,
            TiknasClaimTypes.Name,
            TiknasClaimTypes.SurName,
            TiknasClaimTypes.Role,
            TiknasClaimTypes.Email,
            TiknasClaimTypes.EmailVerified,
            TiknasClaimTypes.PhoneNumber,
            TiknasClaimTypes.PhoneNumberVerified
        };

        RemoteRefreshUrl = "/api/account/dynamic-claims/refresh";
        IsRemoteRefreshEnabled = true;

        ClaimsMap = new Dictionary<string, List<string>>()
        {
            { TiknasClaimTypes.UserName, new List<string> { "preferred_username", "unique_name", ClaimTypes.Name }},
            { TiknasClaimTypes.Name, new List<string> { "given_name", ClaimTypes.GivenName }},
            { TiknasClaimTypes.SurName, new List<string> { "family_name", ClaimTypes.Surname }},
            { TiknasClaimTypes.Role, new List<string> { "role", "roles", ClaimTypes.Role }},
            { TiknasClaimTypes.Email, new List<string> { "email", ClaimTypes.Email }},
        };

        IsDynamicClaimsEnabled = false;
    }
}
