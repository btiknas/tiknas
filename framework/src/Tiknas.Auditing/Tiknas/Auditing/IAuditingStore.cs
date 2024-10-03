using System.Threading.Tasks;

namespace Tiknas.Auditing;

public interface IAuditingStore
{
    Task SaveAsync(AuditLogInfo auditInfo);
}
