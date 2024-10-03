using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Tiknas.AspNetCore.SignalR.SampleHubs;
using Xunit;

namespace Tiknas.AspNetCore.SignalR;

public class TiknasSignalROptions_Tests : TiknasAspNetCoreTestBase<Program>
{
    private readonly TiknasSignalROptions _options;

    public TiknasSignalROptions_Tests()
    {
        _options = GetRequiredService<IOptions<TiknasSignalROptions>>().Value;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<RegularHubClass1>();
        services.AddTransient<RegularHubClass12>();
        services.AddTransient<RegularHubClass2>();
        services.AddTransient<RegularHubClass22>();
    }

    [Fact]
    public void Should_Auto_Add_Maps()
    {
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularHub));
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularTiknasHub));
        _options.Hubs.ShouldNotContain(h => h.HubType == typeof(DisableConventionalRegistrationHub));
        _options.Hubs.ShouldNotContain(h => h.HubType == typeof(DisableAutoHubMapHub));
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularHubClass1));
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularHubClass12));
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularHubClass2));
        _options.Hubs.ShouldContain(h => h.HubType == typeof(RegularHubClass22));
    }
}

[Collection("TiknasAspNetCoreSignalR")]
public class TiknasSignalSameRroutePattern_Tests : TiknasAspNetCoreTestBase<Program>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<RegularHubClass1>();
        services.AddTransient<RegularHubClass12>();
        services.AddTransient<RegularHubClass1, RegularHubClass12>();
    }

    [Fact]
    public void Should_Throw_Exception_If_HubType_Has_Same_RoutePattern()
    {
        TiknasAspNetCoreSignalRTestModule.UseConfiguredEndpointsException.ShouldNotBeNull();
        TiknasAspNetCoreSignalRTestModule.UseConfiguredEndpointsException.Message.ShouldBe($"The hub type {typeof(RegularHubClass12).FullName} is already registered with route pattern {HubRouteAttribute.GetRoutePattern(typeof(RegularHubClass12))}");
    }
}

[Collection("TiknasAspNetCoreSignalR")]
public class TiknasSignalDifferentRroutePattern_Tests : TiknasAspNetCoreTestBase<Program>
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<RegularHubClass2>();
        services.AddTransient<RegularHubClass22>();
        services.AddTransient<RegularHubClass2, RegularHubClass22>();

        services.Configure<TiknasSignalROptions>(options =>
        {
            var firstHub = options.Hubs.FirstOrDefault(x => x.HubType == typeof(RegularHubClass22));
            if (firstHub != null)
            {
                firstHub.RoutePattern = "/signalr-hubs/regular-hub-class-22";
            }

            var lastHub = options.Hubs.LastOrDefault(x => x.HubType == typeof(RegularHubClass22));
            if (lastHub != null)
            {
                lastHub.RoutePattern = "/signalr-hubs/regular-hub-class-22-1";
            }
        });
    }

    [Fact]
    public void Should_Work_If_Same_HubType_Has_Different_RoutePattern()
    {
        TiknasAspNetCoreSignalRTestModule.UseConfiguredEndpointsException.ShouldBeNull();
    }
}
