using System.Collections.Generic;

namespace Tiknas.UI.Navigation.Urls;

public class AppUrlOptions
{
    public ApplicationUrlDictionary Applications { get; }

    public List<string> RedirectAllowedUrls { get; }

    public AppUrlOptions()
    {
        Applications = new ApplicationUrlDictionary();
        RedirectAllowedUrls = new List<string>();
    }
}
