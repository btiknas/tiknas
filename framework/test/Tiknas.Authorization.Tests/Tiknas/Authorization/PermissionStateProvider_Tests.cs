using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Authorization.Permissions;
using Tiknas.Security.Claims;
using Tiknas.SimpleStateChecking;
using Xunit;

namespace Tiknas.Authorization;

public abstract class PermissionStateProvider_Tests : AuthorizationTestBase
{
    protected ISimpleStateCheckerManager<PermissionDefinition> StateCheckerManager { get; }
    protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
    protected ICurrentPrincipalAccessor CurrentPrincipalAccessor { get; }

    public PermissionStateProvider_Tests()
    {
        StateCheckerManager = GetRequiredService<ISimpleStateCheckerManager<PermissionDefinition>>();
        PermissionDefinitionManager = GetRequiredService<IPermissionDefinitionManager>();
        CurrentPrincipalAccessor = GetRequiredService<ICurrentPrincipalAccessor>();
    }
}

public class SpecifyPermissionStateProvider : PermissionStateProvider_Tests
{
    [Fact]
    public async Task PermissionState_Test()
    {
        var myPermission1 = await PermissionDefinitionManager.GetOrNullAsync("MyPermission1");
        myPermission1.ShouldNotBeNull();
        myPermission1.StateCheckers.ShouldContain(x => x.GetType() == typeof(TestRequireEditionPermissionSimpleStateChecker));

        (await StateCheckerManager.IsEnabledAsync(myPermission1)).ShouldBeFalse();

        using (CurrentPrincipalAccessor.Change(new Claim(TiknasClaimTypes.EditionId, Guid.NewGuid().ToString())))
        {
            (await StateCheckerManager.IsEnabledAsync(myPermission1)).ShouldBeTrue();
        }
    }
}

public class GlobalPermissionStateProvider : PermissionStateProvider_Tests
{
    protected override void AfterAddApplication(IServiceCollection services)
    {
        services.Configure<TiknasSimpleStateCheckerOptions<PermissionDefinition>>(options =>
        {
            options.GlobalStateCheckers.Add<TestGlobalRequireRolePermissionSimpleStateChecker>();
        });
    }

    [Fact]
    public async Task Global_PermissionState_Test()
    {
        var myPermission2 = await PermissionDefinitionManager.GetOrNullAsync("MyPermission2");
        myPermission2.ShouldNotBeNull();

        (await StateCheckerManager.IsEnabledAsync(myPermission2)).ShouldBeFalse();

        using (CurrentPrincipalAccessor.Change(new Claim(TiknasClaimTypes.Role, "admin")))
        {
            (await StateCheckerManager.IsEnabledAsync(myPermission2)).ShouldBeTrue();
        }
    }
}
