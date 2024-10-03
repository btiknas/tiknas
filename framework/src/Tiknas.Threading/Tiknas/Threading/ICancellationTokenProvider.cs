using System;
using System.Threading;

namespace Tiknas.Threading;

public interface ICancellationTokenProvider
{
    CancellationToken Token { get; }

    IDisposable Use(CancellationToken cancellationToken);
}
