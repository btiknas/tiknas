using MongoDB.Driver;
using Tiknas.EntityFrameworkCore.TestApp.FourthContext;

namespace Tiknas.MongoDB.TestApp.FourthContext;

/* This dbcontext is just for testing to replace dbcontext from the application using ReplaceDbContextAttribute
 */
public class FourthDbContext : TiknasMongoDbContext, IFourthDbContext
{
    public IMongoCollection<FourthDbContextDummyEntity> FourthDummyEntities => Collection<FourthDbContextDummyEntity>();

}
