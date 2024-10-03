using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Tiknas.AspNetCore.TestBase;

public class TestNoopHostLifetime : IHostLifetime
{
    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task WaitForStartAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
