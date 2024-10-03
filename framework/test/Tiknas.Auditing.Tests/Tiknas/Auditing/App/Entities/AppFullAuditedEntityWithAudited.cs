using System;
using Tiknas.Domain.Entities.Auditing;

namespace Tiknas.Auditing.App.Entities;

[Audited]
public class AppFullAuditedEntityWithAudited : FullAuditedAggregateRoot<Guid>
{
    protected AppFullAuditedEntityWithAudited()
    {
    }

    public AppFullAuditedEntityWithAudited(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}
