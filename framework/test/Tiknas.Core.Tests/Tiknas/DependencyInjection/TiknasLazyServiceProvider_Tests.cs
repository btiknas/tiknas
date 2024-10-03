using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Modularity;
using Tiknas.TestBase.Utils;
using Xunit;

namespace Tiknas.DependencyInjection;

public class TiknasLazyServiceProvider_Tests
{
    [Fact]
    public void LazyServiceProvider_Should_Cache_Services()
    {
        using (var application = TiknasApplicationFactory.Create<TestModule>())
        {
            application.Initialize();

            var lazyServiceProvider = application.ServiceProvider.GetRequiredService<ITiknasLazyServiceProvider>();

            var transientTestService1 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            var transientTestService2 = lazyServiceProvider.LazyGetRequiredService<TransientTestService>();
            transientTestService1.ShouldBeSameAs(transientTestService2);

            var testCounter = application.ServiceProvider.GetRequiredService<ITestCounter>();
            testCounter.GetValue(nameof(TransientTestService)).ShouldBe(1);
        }
    }

    [DependsOn(typeof(TiknasTestBaseModule))]
    private class TestModule : TiknasModule
    {
        public TestModule()
        {
            SkipAutoServiceRegistration = true;
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddType<TransientTestService>();
        }
    }

    private class TransientTestService : ITransientDependency
    {
        public TransientTestService(ITestCounter counter)
        {
            counter.Increment(nameof(TransientTestService));
        }
    }
}
