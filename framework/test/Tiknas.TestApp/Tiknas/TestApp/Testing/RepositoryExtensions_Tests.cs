using System;
using System.Threading.Tasks;
using Tiknas.Domain.Entities;
using Tiknas.Domain.Repositories;
using Tiknas.Modularity;
using Tiknas.TestApp.Domain;
using Xunit;

namespace Tiknas.TestApp.Testing;

public abstract class RepositoryExtensions_Tests<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected readonly IRepository<Person, Guid> PersonRepository;

    protected RepositoryExtensions_Tests()
    {
        PersonRepository = GetRequiredService<IRepository<Person, Guid>>();
    }

    [Fact]
    public async Task EnsureExistsAsync_Test()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var id = Guid.NewGuid();
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await PersonRepository.EnsureExistsAsync(Guid.NewGuid())
            );
            await Assert.ThrowsAsync<EntityNotFoundException>(async () =>
                await PersonRepository.EnsureExistsAsync(x => x.Id == id)
            );

            await PersonRepository.EnsureExistsAsync(TestDataBuilder.UserDouglasId);
            await PersonRepository.EnsureExistsAsync(x => x.Id == TestDataBuilder.UserDouglasId);
        });
    }
}
