using System;
using Tiknas.Domain.Entities.Events.Distributed;
using Tiknas.EventBus;

namespace Tiknas.MultiTenancy;

[Serializable]
[EventName("tiknas.multi_tenancy.tenant.connection_string.updated")]
public class TenantConnectionStringUpdatedEto : EtoBase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string ConnectionStringName { get; set; } = default!;

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }
}
