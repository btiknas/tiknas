namespace Tiknas.Caching;

public interface IDistributedCacheKeyNormalizer
{
    string NormalizeKey(DistributedCacheKeyNormalizeArgs args);
}
