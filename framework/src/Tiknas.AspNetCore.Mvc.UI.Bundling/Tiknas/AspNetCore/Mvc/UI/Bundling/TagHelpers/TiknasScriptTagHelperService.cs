namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class TiknasScriptTagHelperService : TiknasBundleItemTagHelperService<TiknasScriptTagHelper, TiknasScriptTagHelperService>
{
    public TiknasScriptTagHelperService(TiknasTagHelperScriptService resourceService)
        : base(resourceService)
    {
    }
}
