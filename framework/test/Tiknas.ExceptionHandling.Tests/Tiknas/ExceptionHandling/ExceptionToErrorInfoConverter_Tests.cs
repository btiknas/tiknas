using Shouldly;
using Tiknas.AspNetCore.ExceptionHandling;
using Xunit;

namespace Tiknas.ExceptionHandling;

public class ExceptionToErrorInfoConverter_Tests : TiknasExceptionHandlingTestBase
{
    private readonly IExceptionToErrorInfoConverter _exceptionToErrorInfoConverter;

    public ExceptionToErrorInfoConverter_Tests()
    {
        _exceptionToErrorInfoConverter = GetRequiredService<IExceptionToErrorInfoConverter>();
    }

    [Fact]
    public void SendExceptionDataToClientTypes_Test()
    {
        var exception = new TiknasException("test message")
        {
            Data =
            {
                ["my_data"] = "my_data_value",
                ["my_data2"] = 42
            }
        };
        var errorInfo = _exceptionToErrorInfoConverter.Convert(exception);
        errorInfo.Data.ShouldBeNull();

        errorInfo = _exceptionToErrorInfoConverter.Convert(exception, options =>
        {
            options.SendExceptionDataToClientTypes.Add(typeof(TiknasException));
        });
        errorInfo.Data.ShouldNotBeNull();
        errorInfo.Data.Keys.Count.ShouldBe(2);
        errorInfo.Data["my_data"].ShouldBe("my_data_value");
        errorInfo.Data["my_data2"].ShouldBe(42);

        var businessException = new BusinessException("test message")
        {
            Data =
            {
                ["my_data"] = "my_data_value",
                ["my_data2"] = 42
            }
        };
        errorInfo = _exceptionToErrorInfoConverter.Convert(businessException);
        errorInfo.Data.ShouldNotBeNull();
        errorInfo.Data.Keys.Count.ShouldBe(2);
        errorInfo.Data["my_data"].ShouldBe("my_data_value");
        errorInfo.Data["my_data2"].ShouldBe(42);
    }
}
