using System.Threading.Tasks;
using Shouldly;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Settings;

public class SettingProvider_Tests : TiknasIntegratedTest<TiknasSettingsTestModule>
{
    private readonly ISettingProvider _settingProvider;

    public SettingProvider_Tests()
    {
        _settingProvider = GetRequiredService<ISettingProvider>();
    }

    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
    }

    [Fact]
    public async Task Should_Get_Null_If_No_Value_Provided_And_No_Default_Value()
    {
        (await _settingProvider.GetOrNullAsync(TestSettingNames.TestSettingWithoutDefaultValue))
            .ShouldBeNull();
    }

    [Fact]
    public async Task Should_Get_Default_Value_If_No_Value_Provided_And_There_Is_A_Default_Value()
    {
        (await _settingProvider.GetOrNullAsync(TestSettingNames.TestSettingWithDefaultValue))
            .ShouldBe("default-value");
    }
}
