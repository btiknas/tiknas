using System;
using System.Collections.Generic;
using Tiknas.Security.Claims;

namespace Tiknas.AspNetCore.Security.Claims;

public class TiknasClaimsMapOptions
{
    public Dictionary<string, Func<string>> Maps { get; }

    public TiknasClaimsMapOptions()
    {
        Maps = new Dictionary<string, Func<string>>()
            {
                { "sub", () => TiknasClaimTypes.UserId },
                { "role", () => TiknasClaimTypes.Role },
                { "email", () => TiknasClaimTypes.Email },
                { "name", () => TiknasClaimTypes.UserName },
                { "family_name", () => TiknasClaimTypes.SurName },
                { "given_name", () => TiknasClaimTypes.Name }
            };
    }
}
