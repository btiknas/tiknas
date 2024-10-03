using Tiknas.Http;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared.Views.Error;

public class TiknasErrorViewModel
{
    public RemoteServiceErrorInfo ErrorInfo { get; set; } = default!;

    public int HttpStatusCode { get; set; }
}
