namespace Tiknas.Auditing;

public interface IAuditLogScope
{
    AuditLogInfo Log { get; }
}
