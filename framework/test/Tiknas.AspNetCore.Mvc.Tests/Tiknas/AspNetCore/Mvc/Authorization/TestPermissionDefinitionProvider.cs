using Tiknas.Authorization.Permissions;

namespace Tiknas.AspNetCore.Mvc.Authorization;

public class TestPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var testGroup = context.AddGroup("TestGroup");

        testGroup.AddPermission("TestPermission1");
        testGroup.AddPermission("TestPermission2");
    }
}
