using System.Collections.Generic;

namespace Tiknas.AspNetCore.Auditing;

public class TiknasAspNetCoreAuditingOptions
{
    /// <summary>
    /// This is used to disable the <see cref="TiknasAuditingMiddleware"/>,
    /// app.UseAuditing(), for the specified URLs.
    /// <see cref="TiknasAuditingMiddleware"/> will be disabled for URLs
    /// starting with an ignored URL.  
    /// </summary>
    public List<string> IgnoredUrls { get; } = new();
}
