using Tiknas.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;

namespace Tiknas.AspNetCore.Mvc.UI.Bundling.TagHelpers;

public abstract class TiknasBundleTagHelper<TTagHelper, TService> : TiknasTagHelper<TTagHelper, TService>, IBundleTagHelper
    where TTagHelper : TiknasTagHelper<TTagHelper, TService>
    where TService : class, ITiknasTagHelperService<TTagHelper>
{
    public string? Name { get; set; }

    protected TiknasBundleTagHelper(TService service)
        : base(service)
    {

    }

    public virtual string? GetNameOrNull()
    {
        return Name;
    }
}
