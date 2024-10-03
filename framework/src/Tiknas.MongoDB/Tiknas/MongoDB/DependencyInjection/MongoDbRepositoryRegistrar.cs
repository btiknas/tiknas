using System;
using System.Collections.Generic;
using Tiknas.Domain.Repositories;
using Tiknas.Domain.Repositories.MongoDB;

namespace Tiknas.MongoDB.DependencyInjection;

public class MongoDbRepositoryRegistrar : RepositoryRegistrarBase<TiknasMongoDbContextRegistrationOptions>
{
    public MongoDbRepositoryRegistrar(TiknasMongoDbContextRegistrationOptions options)
        : base(options)
    {

    }

    protected override IEnumerable<Type> GetEntityTypes(Type dbContextType)
    {
        return MongoDbContextHelper.GetEntityTypes(dbContextType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType)
    {
        return typeof(MongoDbRepository<,>).MakeGenericType(dbContextType, entityType);
    }

    protected override Type GetRepositoryType(Type dbContextType, Type entityType, Type primaryKeyType)
    {
        return typeof(MongoDbRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType);
    }
}
