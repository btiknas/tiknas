using System;
using Tiknas.Auditing;

namespace Tiknas.Domain.Entities.Events.Distributed.EntitySynchronizers.WithEntityVersion;

public class RemoteBookEto : EntityEto<Guid>, IHasEntityVersion
{
    public int EntityVersion { get; set; }
    
    public int Sold { get; set; }
}