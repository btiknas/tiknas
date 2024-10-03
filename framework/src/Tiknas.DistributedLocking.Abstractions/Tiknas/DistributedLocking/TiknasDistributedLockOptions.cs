namespace Tiknas.DistributedLocking;

public class TiknasDistributedLockOptions
{
    /// <summary>
    /// DistributedLock key prefix.
    /// </summary>
    public string KeyPrefix  { get; set; }
    
    public TiknasDistributedLockOptions()
    {
        KeyPrefix = "";
    }
}