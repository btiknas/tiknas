﻿using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.Modularity;
using Tiknas.TestApp.Domain;
using Xunit;

namespace Tiknas.TestApp.Testing;

public abstract class Repository_Basic_Tests_With_Int_Pk<TStartupModule> : TestAppTestBase<TStartupModule>
    where TStartupModule : ITiknasModule
{
    protected readonly IRepository<EntityWithIntPk, int> EntityWithIntPkRepository;

    protected Repository_Basic_Tests_With_Int_Pk()
    {
        EntityWithIntPkRepository = GetRequiredService<IRepository<EntityWithIntPk, int>>();
    }

    [Fact]
    public virtual async Task FirstOrDefault()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var entity = await EntityWithIntPkRepository.FirstOrDefaultAsync(e => e.Name == "Entity1");
            entity.ShouldNotBeNull();
            entity.Name.ShouldBe("Entity1");
            return Task.CompletedTask;
        });
    }

    [Fact]
    public virtual async Task Get()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var entity = await EntityWithIntPkRepository.GetAsync(1);
            entity.ShouldNotBeNull();
            entity.Name.ShouldBe("Entity1");
        });
    }
}
