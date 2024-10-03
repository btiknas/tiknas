using System;
using System.Threading.Tasks;

namespace Tiknas.Auditing;

public interface IAuditLogSaveHandle : IDisposable
{
    Task SaveAsync();
}
