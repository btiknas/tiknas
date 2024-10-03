using System.Collections.Generic;

namespace Tiknas.AspNetCore.Mvc.UI.Theme.Shared;

public class TiknasErrorPageOptions
{
    public readonly IDictionary<string, string> ErrorViewUrls;

    public TiknasErrorPageOptions()
    {
        ErrorViewUrls = new Dictionary<string, string>();
    }
}
