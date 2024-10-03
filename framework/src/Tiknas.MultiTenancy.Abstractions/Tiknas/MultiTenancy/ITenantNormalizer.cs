namespace Tiknas.MultiTenancy;

public interface ITenantNormalizer
{
    string? NormalizeName(string? name);
}
