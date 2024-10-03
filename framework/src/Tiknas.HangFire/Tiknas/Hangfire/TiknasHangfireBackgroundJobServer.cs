using Hangfire;

namespace Tiknas.Hangfire;

public class TiknasHangfireBackgroundJobServer
{
    public BackgroundJobServer? HangfireJobServer { get; }

    public TiknasHangfireBackgroundJobServer(BackgroundJobServer? hangfireJobServer)
    {
        HangfireJobServer = hangfireJobServer;
    }
}
