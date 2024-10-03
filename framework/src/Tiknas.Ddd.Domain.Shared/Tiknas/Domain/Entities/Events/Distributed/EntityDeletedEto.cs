using System;
using Tiknas.EventBus;
using Tiknas.MultiTenancy;

namespace Tiknas.Domain.Entities.Events.Distributed;

[Serializable]
[GenericEventName(Postfix = ".Deleted")]
public class EntityDeletedEto<TEntityEto> : IEventDataMayHaveTenantId
{
    public TEntityEto Entity { get; set; }

    public EntityDeletedEto(TEntityEto entity)
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
