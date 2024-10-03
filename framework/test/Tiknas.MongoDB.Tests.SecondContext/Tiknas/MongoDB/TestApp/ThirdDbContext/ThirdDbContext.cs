using MongoDB.Driver;

namespace Tiknas.MongoDB.TestApp.ThirdDbContext;

/* This dbcontext is just for testing to replace dbcontext from the application using TiknasDbContextRegistrationOptions.ReplaceDbContext
 */
public class ThirdDbContext : TiknasMongoDbContext, IThirdDbContext
{
    public IMongoCollection<ThirdDbContextDummyEntity> DummyEntities => Collection<ThirdDbContextDummyEntity>();
}
