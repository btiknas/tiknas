using System;
using Tiknas.EventBus;
using Tiknas.MultiTenancy;

namespace Tiknas.Domain.Entities.Events.Distributed;

[Serializable]
[GenericEventName(Postfix = ".Updated")]
public class EntityUpdatedEto<TEntityEto> : IEventDataMayHaveTenantId
{
    public TEntityEto Entity { get; set; }

    public EntityUpdatedEto(TEntityEto entity)
    {
        Entity = entity;
    }

    public virtual bool IsMultiTenant(out Guid? tenantId)
    {
        if (Entity is IMultiTenant multiTenantEntity)
        {
            tenantId = multiTenantEntity.TenantId;
            return true;
        }

        tenantId = null;
        return false;
    }
}
