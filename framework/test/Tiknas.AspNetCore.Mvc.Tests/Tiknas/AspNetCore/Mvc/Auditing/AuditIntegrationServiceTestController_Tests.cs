﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using Shouldly;
using Tiknas.Auditing;
using Xunit;

namespace Tiknas.AspNetCore.Mvc.Auditing;

public class AuditIntegrationServiceTestController_Tests : AspNetCoreMvcTestBase
{
    private readonly TiknasAuditingOptions _options;
    private IAuditingStore _auditingStore;

    public AuditIntegrationServiceTestController_Tests()
    {
        _options = ServiceProvider.GetRequiredService<IOptions<TiknasAuditingOptions>>().Value;
        _auditingStore = ServiceProvider.GetRequiredService<IAuditingStore>();
    }
    
    protected override void ConfigureServices(IServiceCollection services)
    {
        _auditingStore = Substitute.For<IAuditingStore>();
        services.Replace(ServiceDescriptor.Singleton(_auditingStore));
        base.ConfigureServices(services);
    }
    
    [Fact]
    public async Task Should_Write_Audit_Log_For_Controllers_With_IntegrationService_Attribute_If_IsEnabledForIntegrationServices()
    {
        _options.IsEnabledForGetRequests = true;
        _options.IsEnabledForIntegrationServices = true;
        await GetResponseAsync("/integration-api/audit-test/");
        await _auditingStore
            .Received()
            .SaveAsync(
                Arg.Is<AuditLogInfo>(
                    x => x.Actions.Any(
                        a =>
                            a.MethodName == nameof(AuditIntegrationServiceTestController.Get) &&
                            a.ServiceName == typeof(AuditIntegrationServiceTestController).FullName
                    )
                )
            );
    }

    [Fact]
    public async Task Should_Not_Write_Audit_Log_For_Controllers_With_IntegrationService_Attribute()
    {
        _options.IsEnabledForGetRequests = true;
        await GetResponseAsync("/integration-api/audit-test/");
        await _auditingStore.DidNotReceive().SaveAsync(Arg.Any<AuditLogInfo>());
    }
}
