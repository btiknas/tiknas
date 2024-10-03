using Microsoft.AspNetCore.Mvc.RazorPages;
using Tiknas.GlobalFeatures;

namespace Tiknas.AspNetCore.Mvc.GlobalFeatures;

[RequiresGlobalFeature("Not-Enabled-Feature")]
public class DisabledGlobalFeatureTestPage : PageModel
{
    public void OnGet()
    {

    }
}
