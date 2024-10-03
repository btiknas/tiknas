using System.Threading.Tasks;
using Shouldly;
using Tiknas.Data;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.ApplicationConfigurations;

public class ApplicationConfigurationBuilder_Tests : AspNetCoreMvcTestBase
{
    [Fact]
    public async Task ApplicationConfigurationBuilder_GetAsync()
    {
        var applicationConfigurationBuilder = GetRequiredService<ITiknasApplicationConfigurationAppService>();

        var config = await applicationConfigurationBuilder.GetAsync(new ApplicationConfigurationRequestOptions());

        config.Auth.ShouldNotBeNull();
        config.Localization.ShouldNotBeNull();
        config.GetProperty("TestKey").ShouldBe("TestValue");
    }
}
