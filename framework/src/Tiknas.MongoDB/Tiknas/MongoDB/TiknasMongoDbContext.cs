using System.Collections.Generic;
using MongoDB.Driver;
using Tiknas.DependencyInjection;

namespace Tiknas.MongoDB;

public abstract class TiknasMongoDbContext : ITiknasMongoDbContext, ITransientDependency
{
    public ITiknasLazyServiceProvider LazyServiceProvider { get; set; } = default!;

    public IMongoModelSource ModelSource { get; set; } = default!;

    public IMongoClient Client { get; private set; } = default!;

    public IMongoDatabase Database { get; private set; } = default!;

    public IClientSessionHandle? SessionHandle { get; private set; }

    protected internal virtual void CreateModel(IMongoModelBuilder modelBuilder)
    {

    }

    public virtual void InitializeDatabase(IMongoDatabase database, IMongoClient client, IClientSessionHandle? sessionHandle)
    {
        Database = database;
        Client = client;
        SessionHandle = sessionHandle;
    }

    public virtual IMongoCollection<T> Collection<T>()
    {
        return Database.GetCollection<T>(GetCollectionName<T>());
    }

    public virtual void InitializeCollections(IMongoDatabase database)
    {
        Database = database;
        ModelSource.GetModel(this);
    }

    protected virtual string GetCollectionName<T>()
    {
        return GetEntityModel<T>().CollectionName;
    }

    protected virtual IMongoEntityModel GetEntityModel<TEntity>()
    {
        var model = ModelSource.GetModel(this).Entities.GetOrDefault(typeof(TEntity));

        if (model == null)
        {
            throw new TiknasException("Could not find a model for given entity type: " + typeof(TEntity).AssemblyQualifiedName);
        }

        return model;
    }
}
