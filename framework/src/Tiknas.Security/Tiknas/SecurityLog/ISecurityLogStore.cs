using System.Threading.Tasks;

namespace Tiknas.SecurityLog;

public interface ISecurityLogStore
{
    Task SaveAsync(SecurityLogInfo securityLogInfo);
}
