namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class TiknasStyleTagHelperService : TiknasBundleItemTagHelperService<TiknasStyleTagHelper, TiknasStyleTagHelperService>
{
    public TiknasStyleTagHelperService(TiknasTagHelperStyleService resourceService)
        : base(resourceService)
    {
    }
}
