using MongoDB.Driver;
using Tiknas.EntityFrameworkCore.TestApp.FourthContext;

namespace Tiknas.MongoDB.TestApp.FourthContext;

public interface IFourthDbContext : ITiknasMongoDbContext
{
    IMongoCollection<FourthDbContextDummyEntity> FourthDummyEntities { get; }
}
