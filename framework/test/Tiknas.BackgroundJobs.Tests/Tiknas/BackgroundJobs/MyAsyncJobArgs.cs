using System;
using Tiknas.MultiTenancy;

namespace Tiknas.BackgroundJobs;

public class MyAsyncJobArgs: IMultiTenant
{
    public string Value { get; set; }
    
    public Guid? TenantId { get; }

    public MyAsyncJobArgs()
    {

    }

    public MyAsyncJobArgs(string value, Guid? tenantId = null)
    {
        Value = value;
        TenantId = tenantId;
    }
}
