namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public class TiknasScriptBundleTagHelperService : TiknasBundleTagHelperService<TiknasScriptBundleTagHelper, TiknasScriptBundleTagHelperService>
{
    public TiknasScriptBundleTagHelperService(TiknasTagHelperScriptService resourceHelper)
        : base(resourceHelper)
    {
    }
}
