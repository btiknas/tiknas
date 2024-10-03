using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tiknas.AspNetCore.Mvc.UI.RazorPages;
using Tiknas.Features;

namespace Tiknas.AspNetCore.Mvc.Features;

public class FeatureTestPage : TiknasPageModel
{
    [RequiresFeature("AllowedFeature")]
    public Task OnGetAllowedFeatureAsync()
    {
        return Task.CompletedTask;
    }

    [RequiresFeature("NotAllowedFeature")]
    public ObjectResult OnGetNotAllowedFeature()
    {
        return new ObjectResult(42);
    }

    public ObjectResult OnGetNoFeature()
    {
        return new ObjectResult(42);
    }
}
