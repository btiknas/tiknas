﻿using Shouldly;
using System.Threading.Tasks;
using Tiknas.Authorization.Permissions;
using Tiknas.Authorization.TestServices;
using Xunit;

namespace Tiknas.Authorization;

public class Authorization_Tests : AuthorizationTestBase
{
    private readonly IMyAuthorizedService1 _myAuthorizedService1;
    private readonly IMySimpleAuthorizedService _simpleAuthorizedService;
    private readonly IMyAuthorizedServiceWithRole _myAuthorizedServiceWithRole;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;

    public Authorization_Tests()
    {
        _myAuthorizedService1 = GetRequiredService<IMyAuthorizedService1>();
        _simpleAuthorizedService = GetRequiredService<IMySimpleAuthorizedService>();
        _myAuthorizedServiceWithRole = GetRequiredService<IMyAuthorizedServiceWithRole>();
        _permissionDefinitionManager = GetRequiredService<IPermissionDefinitionManager>();
    }

    [Fact]
    public async Task Should_Not_Allow_To_Call_Authorized_Method_For_Anonymous_User()
    {
        await Assert.ThrowsAsync<TiknasAuthorizationException>(async () =>
        {
            await _simpleAuthorizedService.ProtectedByClassAsync();
        });
    }

    [Fact]
    public async Task Should_Allow_To_Call_Anonymous_Method_For_Anonymous_User()
    {
        (await _simpleAuthorizedService.AnonymousAsync()).ShouldBe(42);
    }

    [Fact]
    public async Task Should_Not_Allow_To_Call_Method_If_Has_No_Permission_ProtectedByClass()
    {
        await Assert.ThrowsAsync<TiknasAuthorizationException>(async () =>
        {
            await _myAuthorizedService1.ProtectedByClass();
        });
    }

    [Fact]
    public async Task Should_Not_Allow_To_Call_Method_If_Has_No_Permission_ProtectedByClass_Async()
    {
        await Assert.ThrowsAsync<TiknasAuthorizationException>(async () =>
        {
            await _myAuthorizedService1.ProtectedByClassAsync();
        });
    }

    [Fact]
    public async Task Should_Allow_To_Call_Anonymous_Method()
    {
        (await _myAuthorizedService1.Anonymous()).ShouldBe(42);
    }

    [Fact]
    public async Task Should_Allow_To_Call_Anonymous_Method_Async()
    {
        (await _myAuthorizedService1.AnonymousAsync()).ShouldBe(42);
    }

    [Fact]
    public async Task Should_Permission_Definition_GetGroup()
    {
        (await _permissionDefinitionManager.GetGroupsAsync()).Count.ShouldBe(1);
    }

    [Fact]
    public async Task Should_Not_Allow_To_Call_Method_If_Has_No_Role_ProtectedByRole_Async()
    {
        await Assert.ThrowsAsync<TiknasAuthorizationException>(async () =>
        {
            await _myAuthorizedServiceWithRole.ProtectedByAnotherRole().ConfigureAwait(false);
        }).ConfigureAwait(true);
    }

    [Fact]
    public async Task Should_Allow_To_Call_Method_If_Has_No_Role_ProtectedByRole_Async()
    {
        int result = await _myAuthorizedServiceWithRole.ProtectedByRole().ConfigureAwait(true);
        result.ShouldBe(42);
    }


    [Fact]
    public async Task Should_Not_Allow_To_Call_Method_If_Has_No_Role_ProtectedByScheme_Async()
    {
        await Assert.ThrowsAsync<TiknasAuthorizationException>(async () =>
        {
            await _myAuthorizedServiceWithRole.ProtectedByScheme().ConfigureAwait(false);
        }).ConfigureAwait(true);
    }
}
