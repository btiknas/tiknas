using System;
using Tiknas.Domain.Entities.Events.Distributed;
using Tiknas.EventBus;

namespace Tiknas.MultiTenancy;

[Serializable]
[EventName("tiknas.multi_tenancy.tenant.created")]
public class TenantCreatedEto : EtoBase
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;
}
