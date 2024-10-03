using System;
using System.Threading;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.TestApp.Testing;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.MongoDB.Uow;

[Collection(MongoTestCollection.Name)]
public class UnitOfWork_CancellationToken_Tests : TestAppTestBase<TiknasMongoDbTestModule>
{
    [Fact]
    public async Task Should_Cancel_Test()
    {
        using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin(isTransactional: true))
        {
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                var cst = new CancellationTokenSource();
                cst.Cancel();

                await GetRequiredService<IBasicRepository<Person, Guid>>().InsertAsync(new Person(Guid.NewGuid(), "Adam", 42));

                await uow.CompleteAsync(cst.Token);
            });
        }
    }
}
