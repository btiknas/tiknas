using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Domain.Repositories;
using Tiknas.MongoDB.TestApp.FifthContext;
using Tiknas.MongoDB.TestApp.FourthContext;
using Tiknas.MongoDB.TestApp.ThirdDbContext;
using Tiknas.MultiTenancy;
using Tiknas.TestApp.MongoDb;
using Tiknas.TestApp.MongoDB;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.MongoDB;

[Collection(MongoTestCollection.Name)]
public class DbContext_Replace_Tests : MongoDbTestBase
{
    private readonly IMongoDbContextTypeProvider _dbContextTypeProvider;

    public DbContext_Replace_Tests()
    {
        _dbContextTypeProvider = GetRequiredService<IMongoDbContextTypeProvider>();
    }

    [Fact]
    public void Should_Replace_DbContext()
    {
        _dbContextTypeProvider.GetDbContextType(typeof(IThirdDbContext)).ShouldBe(typeof(TestAppMongoDbContext));
        _dbContextTypeProvider.GetDbContextType(typeof(IFourthDbContext)).ShouldBe(typeof(TestAppMongoDbContext));

        (ServiceProvider.GetRequiredService<IThirdDbContext>() is TestAppMongoDbContext).ShouldBeTrue();
        (ServiceProvider.GetRequiredService<IFourthDbContext>() is TestAppMongoDbContext).ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Replace_DbContext_By_Host_And_Tenant()
    {
        _dbContextTypeProvider.GetDbContextType(typeof(IFifthDbContext)).ShouldBe(typeof(HostTestAppDbContext));
        (ServiceProvider.GetRequiredService<IFifthDbContext>() is HostTestAppDbContext).ShouldBeTrue();

        using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin())
        {
            var instance1 = await GetRequiredService<IFifthDbContextDummyEntityRepository>().GetDbContextAsync();
            (instance1 is HostTestAppDbContext).ShouldBeTrue();

            var instance2 = await GetRequiredService<IFifthDbContextMultiTenantDummyEntityRepository>().GetDbContextAsync();
            (instance2 is HostTestAppDbContext).ShouldBeTrue();

            instance1.ShouldBe(instance2);

            await uow.CompleteAsync();
        }

        using (GetRequiredService<ICurrentTenant>().Change(Guid.NewGuid()))
        {
            _dbContextTypeProvider.GetDbContextType(typeof(IFifthDbContext)).ShouldBe(typeof(TenantTestAppDbContext));
            (ServiceProvider.GetRequiredService<IFifthDbContext>() is TenantTestAppDbContext).ShouldBeTrue();

            using (var uow = GetRequiredService<IUnitOfWorkManager>().Begin())
            {
                var instance1 = await GetRequiredService<IFifthDbContextDummyEntityRepository>().GetDbContextAsync();
                (instance1 is HostTestAppDbContext).ShouldBeTrue();

                // Multi-tenant entities use tenant DbContext
                var instance2 = await GetRequiredService<IFifthDbContextMultiTenantDummyEntityRepository>().GetDbContextAsync();
                (instance2 is TenantTestAppDbContext).ShouldBeTrue();

                await uow.CompleteAsync();
            }
        }
    }
}
