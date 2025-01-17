﻿using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Tiknas.AspNetCore.Authentication.OAuth.Claims;

public class MultipleClaimAction : ClaimAction
{
    public MultipleClaimAction(string claimType, string jsonKey)
        : base(claimType, jsonKey)
    {

    }

    public override void Run(JsonElement userData, ClaimsIdentity identity, string issuer)
    {
        JsonElement prop;

        if (!userData.TryGetProperty(ValueType, out prop))
            return;

        if (prop.ValueKind == JsonValueKind.Null)
        {
            return;
        }

        Claim claim;
        switch (prop.ValueKind)
        {
            case JsonValueKind.String:
                claim = new Claim(ClaimType, prop.GetString()!, ValueType, issuer);
                if (!identity.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                {
                    identity.AddClaim(claim);
                }
                break;
            case JsonValueKind.Array:
                foreach (var arramItem in prop.EnumerateArray())
                {
                    claim = new Claim(ClaimType, arramItem.GetString()!, ValueType, issuer);
                    if (!identity.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value))
                    {
                        identity.AddClaim(claim);
                    }
                }
                break;
            default:
                throw new TiknasException("Unhandled JsonValueKind: " + prop.ValueKind);
        }
    }
}
