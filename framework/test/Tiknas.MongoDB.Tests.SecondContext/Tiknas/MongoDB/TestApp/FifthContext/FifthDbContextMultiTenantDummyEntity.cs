using System;
using Tiknas.Domain.Entities;
using Tiknas.MultiTenancy;

namespace Tiknas.MongoDB.TestApp.FifthContext;

public class FifthDbContextMultiTenantDummyEntity : AggregateRoot<Guid>, IMultiTenant
{
    public string Value { get; set; }

    public Guid? TenantId { get; set; }
}
