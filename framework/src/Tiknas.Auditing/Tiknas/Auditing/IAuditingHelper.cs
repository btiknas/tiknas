using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tiknas.Auditing;

//TODO: Move ShouldSaveAudit & IsEntityHistoryEnabled and rename to IAuditingFactory
public interface IAuditingHelper
{
    bool ShouldSaveAudit(MethodInfo? methodInfo, bool defaultValue = false, bool ignoreIntegrationServiceAttribute = false);

    bool IsEntityHistoryEnabled(Type entityType, bool defaultValue = false);

    AuditLogInfo CreateAuditLogInfo();

    AuditLogActionInfo CreateAuditLogAction(
        AuditLogInfo auditLog,
        Type? type,
        MethodInfo method,
        object?[] arguments
    );

    AuditLogActionInfo CreateAuditLogAction(
        AuditLogInfo auditLog,
        Type? type,
        MethodInfo method,
        IDictionary<string, object?> arguments
    );
}
