using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Tiknas.Auditing;

public class TiknasAuditingOptions_Tests : TiknasAuditingTestBase
{
    private const string ApplicationName = "TEST_APP_NAME";
    
    protected override void SetTiknasApplicationCreationOptions(TiknasApplicationCreationOptions options)
    {
        base.SetTiknasApplicationCreationOptions(options);
        options.ApplicationName = ApplicationName;
    }
    
    [Fact]
    public void Should_Set_Application_Name_From_Global_Application_Name_By_Default()
    {
        var options = GetRequiredService<IOptions<TiknasAuditingOptions>>().Value;
        options.ApplicationName.ShouldBe(ApplicationName);
    }
}