using Shouldly;
using Tiknas.AspNetCore.ExceptionHandling;
using Tiknas.Authorization;
using Tiknas.Localization;
using Xunit;

namespace Tiknas.Features;

public class FeatureCheckerExtensions_Tests : FeatureTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public FeatureCheckerExtensions_Tests()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void Test_TiknasAuthorizationException_Localization()
    {
        using (CultureHelper.Use("zh-Hans"))
        {
            var exception = new TiknasAuthorizationException(code: TiknasFeatureErrorCodes.FeatureIsNotEnabled)
                .WithData("FeatureName", "my_feature_name");
            var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("功能未启用: my_feature_name");

            exception = new TiknasAuthorizationException(code: TiknasFeatureErrorCodes.AllOfTheseFeaturesMustBeEnabled)
                .WithData("FeatureNames", "my_feature_name, my_feature_name2");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("必要的功能未启用. 这些功能需要启用: my_feature_name, my_feature_name2");

            exception = new TiknasAuthorizationException(code: TiknasFeatureErrorCodes.AtLeastOneOfTheseFeaturesMustBeEnabled)
                .WithData("FeatureNames", "my_feature_name, my_feature_name2");
            errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
            errorInfo.Message.ShouldBe("必要的功能未启用. 需要启用这些功能中的一项：my_feature_name, my_feature_name2");
        }
    }
}
