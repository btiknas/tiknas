using Shouldly;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.Authorization;
using Tiknas.Localization;
using Xunit;

namespace Microsoft.AspNetCore.Authorization;

public class TiknasAuthorizationServiceExtensions_Tests : AuthorizationTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public TiknasAuthorizationServiceExtensions_Tests()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void Test_TiknasAuthorizationException_Localization()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGranted);
            var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略尚未授予。");

            exception = new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGrantedWithPolicyName)
                .WithData("PolicyName", "my_policy_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略尚未授予： my_policy_name");

            exception = new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenPolicyHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的策略未授予提供的资源：my_resource_name");

            exception = new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenRequirementHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的要求未授予提供的资源：my_resource_name");

            exception = new TiknasAuthorizationException(code: TiknasAuthorizationErrorCodes.GivenRequirementsHasNotGrantedForGivenResource)
                .WithData("ResourceName", "my_resource_name");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("授权失败！提供的要求未授予提供的资源：my_resource_name");
        }
    }
}
