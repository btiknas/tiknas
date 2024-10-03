using Shouldly;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.Localization;
using Xunit;

namespace Tiknas.GlobalFeatures;

public class TiknasGlobalFeatureNotEnableException_Localization_Test : GlobalFeatureTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public TiknasGlobalFeatureNotEnableException_Localization_Test()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void TiknasAuthorizationException_Localization()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = new TiknasGlobalFeatureNotEnabledException(code: TiknasGlobalFeatureErrorCodes.GlobalFeatureIsNotEnabled)
                .WithData("ServiceName", "MyService")
                .WithData("GlobalFeatureName", "TestFeature"); ;
            var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("'MyService' 服务需要启用 'TestFeature'。");
        }
    }
}
