using System;
using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Tiknas.Modularity;
using Tiknas.TestBase;
using Tiknas.Uow;
using Xunit;

namespace Tiknas.Data;

public class DataSeederExtensions_Tests : TiknasIntegratedTest<DataSeederExtensions_Tests.TestModule>
{
    private IDataSeeder _dataSeeder;

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _dataSeeder = Substitute.For<IDataSeeder>();
        services.Replace(ServiceDescriptor.Singleton(_dataSeeder));
        base.AfterAddApplication(services);
    }

    [Fact]
    public void SeedInSeparateUowAsync()
    {
        var tenantId = Guid.NewGuid();
        _dataSeeder.SeedInSeparateUowAsync(tenantId, new TiknasUnitOfWorkOptions(true, IsolationLevel.Serializable, 888), true);

        _dataSeeder.Received().SeedAsync(Arg.Is<DataSeedContext>(x => x.TenantId == tenantId &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUow].To<bool>() == true &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<TiknasUnitOfWorkOptions>().IsTransactional == true &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<TiknasUnitOfWorkOptions>().IsolationLevel == IsolationLevel.Serializable &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowOptions].As<TiknasUnitOfWorkOptions>().Timeout == 888 &&
                                                                      x.Properties[DataSeederExtensions.SeedInSeparateUowRequiresNew].To<bool>() == true));
    }

    [DependsOn(typeof(TiknasDataModule))]
    public class TestModule : TiknasModule
    {

    }
}
