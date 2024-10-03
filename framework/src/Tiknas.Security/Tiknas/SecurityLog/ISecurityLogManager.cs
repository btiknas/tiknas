using System;
using System.Threading.Tasks;

namespace Tiknas.SecurityLog;

public interface ISecurityLogManager
{
    Task SaveAsync(Action<SecurityLogInfo>? saveAction = null);
}
