using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Logging;
using Tiknas.Modularity.PlugIns;
using Xunit;

namespace Tiknas.Modularity;

public class ModuleLoader_Tests
{
    [Fact]
    public void Should_Load_Modules_By_Dependency_Order()
    {
        var moduleLoader = new ModuleLoader();
        var modules = moduleLoader.LoadModules(
            new ServiceCollection()
                .AddSingleton<IInitLoggerFactory>(new DefaultInitLoggerFactory()),
            typeof(MyStartupModule),
            new PlugInSourceList()
        );
        modules.Length.ShouldBe(2);
        modules[0].Type.ShouldBe(typeof(IndependentEmptyModule));
        modules[1].Type.ShouldBe(typeof(MyStartupModule));
        modules[1].Assembly.ShouldBe(typeof(MyStartupModule).Assembly);
        modules[1].AllAssemblies.Length.ShouldBe(2);
        modules[1].AllAssemblies[0].ShouldBe(typeof(ITiknasApplication).Assembly);
        modules[1].AllAssemblies[1].ShouldBe(typeof(MyStartupModule).Assembly);
    }

    [DependsOn(typeof(IndependentEmptyModule))]
    [AdditionalAssembly(typeof(ITiknasApplication))]
    public class MyStartupModule : TiknasModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {

        }
    }
}
