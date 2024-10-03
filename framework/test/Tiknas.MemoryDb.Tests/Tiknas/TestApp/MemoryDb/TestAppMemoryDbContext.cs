using System;
using System.Collections.Generic;
using Tiknas.MemoryDb;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;

namespace Tiknas.TestApp.MemoryDb;

public class TestAppMemoryDbContext : MemoryDbContext
{
    private static readonly Type[] EntityTypeList = {
            typeof(Person),
            typeof(EntityWithIntPk),
            typeof(Product),
            typeof(AppEntityWithNavigations)
    };

    public override IReadOnlyList<Type> GetEntityTypes()
    {
        return EntityTypeList;
    }
}
