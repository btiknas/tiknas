using System;

namespace Tiknas.Security.Claims;

[Serializable]
public class TiknasDynamicClaim
{
    public string Type { get; set; }

    public string? Value { get; set; }

    public TiknasDynamicClaim(string type, string? value)
    {
        Type = type;
        Value = value;
    }
}
