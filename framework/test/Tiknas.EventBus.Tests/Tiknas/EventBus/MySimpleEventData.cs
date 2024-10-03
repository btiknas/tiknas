using System;
using Tiknas.MultiTenancy;

namespace Tiknas.EventBus;

public class MySimpleEventData : IMultiTenant
{
    public int Value { get; set; }

    public Guid? TenantId { get; }

    public MySimpleEventData(int value, Guid? tenantId = null)
    {
        Value = value;
        TenantId = tenantId;
    }
}
