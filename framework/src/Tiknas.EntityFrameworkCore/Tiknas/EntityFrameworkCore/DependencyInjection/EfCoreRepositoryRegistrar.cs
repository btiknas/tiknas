using System;
using System.Collections.Generic;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.EntityFrameworkCore;

namespace Tiknas.EntityFrameworkCore.DependencyInjection;

public class EfCoreRepositoryRegistrar : RepositoryRegistrarBase<TiknasDbContextRegistrationOptions>
{
    public EfCoreRepositoryRegistrar(TiknasDbContextRegistrationOptions options)
        : base(options)
    {

    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return DbContextHelper.GetEntityTypes(dbContextType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(EfCoreRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(EfCoreRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
