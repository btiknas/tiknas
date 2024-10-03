using System;

namespace Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithoutEntityVersion;

public class RemoteAuthorEto : EntityEto<Guid>
{
    public string Name { get; set; }
}