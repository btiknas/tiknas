namespace Tiknas.DistributedLocking;

public interface IDistributedLockKeyNormalizer
{
    string NormalizeKey(string name);
    
}