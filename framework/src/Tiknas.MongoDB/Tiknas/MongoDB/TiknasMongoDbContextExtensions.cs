namespace Tiknas.MongoDB;

public static class TiknasMongoDbContextExtensions
{
    public static TiknasMongoDbContext ToTiknasMongoDbContext(this ITiknasMongoDbContext dbContext)
    {
        var tiknasMongoDbContext = dbContext as TiknasMongoDbContext;

        if (tiknasMongoDbContext == null)
        {
            throw new TiknasException($"The type {dbContext.GetType().AssemblyQualifiedName} should be convertable to {typeof(TiknasMongoDbContext).AssemblyQualifiedName}!");
        }

        return tiknasMongoDbContext;
    }
}
