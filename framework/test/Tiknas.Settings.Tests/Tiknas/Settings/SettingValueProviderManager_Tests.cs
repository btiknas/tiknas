using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.DependencyInjection;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Settings;

public class SettingValueProviderManager_Tests: TiknasIntegratedTest<TiknasSettingsTestModule>
{
    private readonly ISettingValueProviderManager _settingValueProviderManager;

    public SettingValueProviderManager_Tests()
    {
        _settingValueProviderManager = GetRequiredService<ISettingValueProviderManager>();
    }
    
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        options.UseAutofac();
        options.Services.Configure<TiknasSettingOptions>(settingOptions =>
        {
            settingOptions.ValueProviders.Add<TestDuplicateSettingValueProvider>();
        });
    }
    
    [Fact]
    public void Should_Throw_Exception_If_Duplicate_Provider_Name_Detected()
    {
        var exception = Assert.Throws<TiknasException>(() =>
        {
            var providers = _settingValueProviderManager.Providers;
        });
        
        exception.Message.ShouldBe($"Duplicate setting value provider name detected: {TestDuplicateSettingValueProvider.ProviderName}. Providers:{Environment.NewLine}{typeof(TestDuplicateSettingValueProvider).FullName}{Environment.NewLine}{typeof(TestSettingValueProvider).FullName}");
    }
}

public class TestDuplicateSettingValueProvider : ISettingValueProvider, ITransientDependency
{
    public const string ProviderName = "Test";


    public string Name => ProviderName;

    public TestDuplicateSettingValueProvider()
    {
    }

    public Task<string> GetOrNullAsync(SettingDefinition setting)
    {
        throw new NotImplementedException();
    }

    public Task<List<SettingValue>> GetAllAsync(SettingDefinition[] settings)
    {
        throw new NotImplementedException();
    }
}