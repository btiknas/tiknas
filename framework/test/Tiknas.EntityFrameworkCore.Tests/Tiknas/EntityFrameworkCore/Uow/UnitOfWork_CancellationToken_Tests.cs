using System;
using System.Threading;
using System.Threading.Tasks;
using Tiknas.Domain.Repositories;
using Tiknas.TestApp.Domain;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.EntityFrameworkCore.Uow;

public class UnitOfWork_CancellationToken_Tests : EntityFrameworkCoreTestBase
{
    [Fact]
    public async Task Should_Cancel_Test()
    {
        using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin(isTransactional: true))
        {
            await Assert.ThrowsAsync<TaskCanceledException>(async () =>
            {
                var cst = new CancellationTokenSource();
                cst.Cancel();

                await GetRequiredService<IBasicRepository<Person, Guid>>().InsertAsync(new Person(Guid.NewGuid(), "Adam", 42));

                await uow.CompleteAsync(cst.Token);
            });
        }
    }
}
