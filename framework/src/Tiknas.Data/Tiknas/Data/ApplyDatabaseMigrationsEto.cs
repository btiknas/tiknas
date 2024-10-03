using System;
using Tiknas.Domain.Entities.Events.Distributed;
using Tiknas.EventBus;

namespace Tiknas.Data;

[Serializable]
[EventName("tiknas.data.apply_database_migrations")]
public class ApplyDatabaseMigrationsEto : EtoBase
{
    public Guid? TenantId { get; set; }

    public string DatabaseName { get; set; } = default!;
}
