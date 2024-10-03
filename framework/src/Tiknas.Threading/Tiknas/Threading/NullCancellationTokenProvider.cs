using System.Threading;

namespace Tiknas.Threading;

public class NullCancellationTokenProvider : CancellationTokenProviderBase
{
    public static NullCancellationTokenProvider Instance { get; } = new();

    public override CancellationToken Token => OverrideValue?.CancellationToken ?? CancellationToken.None;

    private NullCancellationTokenProvider()
        : base(new AmbientDataContextAmbientScopeProvider<CancellationTokenOverride>(new AsyncLocalAmbientDataContext()))
    {
    }
}
