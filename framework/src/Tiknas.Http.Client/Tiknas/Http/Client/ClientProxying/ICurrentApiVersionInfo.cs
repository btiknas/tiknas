using System;

namespace Tiknas.Http.Client.ClientProxying;

public interface ICurrentApiVersionInfo
{
    ApiVersionInfo? ApiVersionInfo { get; }

    IDisposable Change(ApiVersionInfo? apiVersionInfo);
}
