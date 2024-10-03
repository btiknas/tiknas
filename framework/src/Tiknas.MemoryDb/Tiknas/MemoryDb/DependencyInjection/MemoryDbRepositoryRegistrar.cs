using System;
using System.Collections.Generic;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.MemoryDb;

namespace Tiknas.MemoryDb.DependencyInjection;

public class MemoryDbRepositoryRegistrar : RepositoryRegistrarBase<TiknasMemoryDbContextRegistrationOptions>
{
    public MemoryDbRepositoryRegistrar(TiknasMemoryDbContextRegistrationOptions options)
        : base(options)
    {
    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        var memoryDbContext = (MemoryDbContext)Activator.CreateInstance(dbContextType)!;
        return memoryDbContext.GetEntityTypes();
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(MemoryDbRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(MemoryDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
