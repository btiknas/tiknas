using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tiknas.SecurityLog;
using Tiknas.TestBase;
using Xunit;

namespace Tiknas.Security.SecurityLog;

public class SecurityLogManager_Tests : TiknasIntegratedTest<TiknasSecurityTestModule>
{
    private readonly ISecurityLogManager _securityLogManager;

    private ISecurityLogStore _auditingStore;

    public SecurityLogManager_Tests()
    {
        _securityLogManager = GetRequiredService<ISecurityLogManager>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        _auditingStore = Substitute.For<ISecurityLogStore>();
        services.AddSingleton(_auditingStore);
    }

    [Fact]
    public async Task SaveAsync()
    {
        await _securityLogManager.SaveAsync(securityLog =>
        {
            securityLog.Identity = "Test";
            securityLog.Action = "Test-Action";
            securityLog.UserName = "Test-User";
        });

        await _auditingStore.Received().SaveAsync(Arg.Is<SecurityLogInfo>(log =>
            log.ApplicationName == "TiknasSecurityTest" &&
            log.Identity == "Test" &&
            log.Action == "Test-Action" &&
            log.UserName == "Test-User"));
    }
}
