using Microsoft.AspNetCore.Mvc.RazorPages;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature(TiknasAspNetCoreMvcTestFeature1.Name)]
public class EnabledGlobalFeatureTestPage : PageModel
{
    public void OnGet()
    {

    }
}
