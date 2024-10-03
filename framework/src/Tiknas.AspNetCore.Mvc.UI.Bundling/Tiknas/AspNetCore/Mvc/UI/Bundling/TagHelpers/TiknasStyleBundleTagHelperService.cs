namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class TiknasStyleBundleTagHelperService : TiknasBundleTagHelperService<TiknasStyleBundleTagHelper, TiknasStyleBundleTagHelperService>
{
    public TiknasStyleBundleTagHelperService(TiknasTagHelperStyleService resourceHelper)
        : base(resourceHelper)
    {
    }
}
