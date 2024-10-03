using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Modularity;
using Xunit;

namespace Tiknas.DependencyInjection;

public class AutoServiceRegistration_Tests
{
    [Fact]
    public async Task AutoServiceRegistration_Should_Not_Duplicate_Test_Async()
    {
        using (var application = await TiknasApplicationFactory.CreateAsync<TestModule>())
        {
            //Act
            await application.InitializeAsync();

            //Assert
            var services = application.ServiceProvider.GetServices<TestService>().ToList();
            services.Count.ShouldBe(1);
        }
    }

    [Fact]
    public void AutoServiceRegistration_Should_Not_Duplicate_Test()
    {
        using (var application = TiknasApplicationFactory.Create<TestModule>())
        {
            //Act
            application.Initialize();

            //Assert
            var services = application.ServiceProvider.GetServices<TestService>().ToList();
            services.Count.ShouldBe(1);
        }
    }
}

[DependsOn(typeof(TestDependedModule))]
public class TestModule : TiknasModule
{

}

public class TestDependedModule : TiknasModule
{

}

public class TestService : ITransientDependency
{

}
