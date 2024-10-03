using System;
using Tiknas.EventBus;

namespace Tiknas.Data;

[Serializable]
[EventName("tiknas.data.applied_database_migrations")]
public class AppliedDatabaseMigrationsEto
{
    public string DatabaseName { get; set; } = default!;
    public Guid? TenantId { get; set; }
}