using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Tiknas.Modularity;
using Tiknas.Tracing;
using Xunit;

namespace Tiknas.CorrelationIdProvider;

public class CorrelationIdProvider_Tests
{
    [Fact]
    public async Task Test()
    {
        using (var application = await TiknasApplicationFactory.CreateAsync<IndependentEmptyModule>())
        {
            await application.InitializeAsync();

            var correlationIdProvider = application.ServiceProvider.GetRequiredService<ICorrelationIdProvider>();

            correlationIdProvider.Get().ShouldBeNull();

            var correlationId = Guid.NewGuid().ToString("N");
            using (correlationIdProvider.Change(correlationId))
            {
                correlationIdProvider.Get().ShouldBe(correlationId);
            }

            correlationIdProvider.Get().ShouldBeNull();
        }
    }
}
