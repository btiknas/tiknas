using System.Collections.Generic;

namespace Tiknas.AspNetCore.Uow;

public class TiknasAspNetCoreUnitOfWorkOptions
{
    /// <summary>
    /// This is used to disable the <see cref="TiknasUnitOfWorkMiddleware"/>,
    /// app.UseUnitOfWork(), for the specified URLs.
    /// <see cref="TiknasUnitOfWorkMiddleware"/> will be disabled for URLs
    /// starting with an ignored URL.  
    /// </summary>
    public List<string> IgnoredUrls { get; } = new List<string>();
}
